using UnityEngine;

public abstract class SimpleAttack : GroundAttack
{
    [SerializeField] public Transform AttackPoint;
    [Header("Optional")]
    [SerializeField] public ForceApplier2D ForceApplier;
    // [SerializeField] public Transform[] fxsOnStart;
    // [SerializeField] public Transform[] fxsOnPerform;
    [SerializeField] public Transform[] fxsOnImpact;

    public abstract Collider2D[] ObjectsImpacted();
    public abstract Vector2 AttackForceDirection(Vector2 closestPoint);
    // public abstract void ApplyForce(Rigidbody2D targetRigidbody, Vector2 closestPoint);

    public override void PerformAttack()
    {
        foreach(Collider2D objectImpacted in ObjectsImpacted())
        {
            foreach (int damageableLayer in attackingEntity.DamageableLayers)
            {
                if(objectImpacted.gameObject.layer == damageableLayer){
                    IDamageTaker damageTaker = objectImpacted.GetComponentInChildren<IDamageTaker>();
                    Rigidbody2D targetRigidbody = objectImpacted.GetComponent<Rigidbody2D>();
                    Vector2 closestPoint = objectImpacted.ClosestPoint(AttackPoint.position);
                    SetEffects(closestPoint);
                    if(targetRigidbody && ForceApplier){
                        EntityStatus2D targetStatus = objectImpacted.GetComponent<EntityStatus2D>();
                        ForceApplier.ApplyForce(targetRigidbody,(Vector2)AttackPoint.position,AttackForceDirection(closestPoint));
                        if(targetStatus) targetStatus.ApplyForce();
                    }
                    if(damageTaker != null) damageTaker.TakeDamage(attackData.Damage,closestPoint);
                    OnAttackConnected?.Invoke();
                }
            }          
        }
    }

    void SetEffects(Vector2 closestPoint){
        foreach (Transform fx in fxsOnImpact){
            fx.position = closestPoint;
        }
    }        
}
