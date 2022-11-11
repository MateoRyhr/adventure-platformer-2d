using UnityEngine;
using UnityEngine.Events;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private AttackData _attackData;
    [SerializeField] private int[] _damageableLayers;
    [Header("Optional")]
    [SerializeField] public ForceApplier2D ForceApplier;
    // [SerializeField] public Transform[] fxsOnStart;
    // [SerializeField] public Transform[] fxsOnPerform;
    [SerializeField] public Transform[] fxsOnImpact;

    public UnityEvent OnHit;
    
    public abstract Collider2D[] ObjectsImpacted();
    public abstract void Impulse(Vector2 direction);
    public abstract Vector2 ImpactForceDirection(Vector2 closestPoint);

    public void Hit(){
        foreach(Collider2D objectImpacted in ObjectsImpacted())
        {
            foreach (int damageableLayer in _damageableLayers)
            {
                if(objectImpacted.gameObject.layer == damageableLayer){
                    IDamageTaker damageTaker = objectImpacted.GetComponentInChildren<IDamageTaker>();
                    Vector2 closestPoint = objectImpacted.ClosestPoint(transform.position);
                    Rigidbody2D targetRigidbody = objectImpacted.GetComponent<Rigidbody2D>();
                    SetEffects(closestPoint);
                    if(targetRigidbody && ForceApplier){
                        EntityStatus2D targetStatus = objectImpacted.GetComponent<EntityStatus2D>();
                        ForceApplier.ApplyForce(targetRigidbody,transform.position,ImpactForceDirection(closestPoint));
                        if(targetStatus) targetStatus.ApplyForce();
                    }
                    if(damageTaker != null) damageTaker.TakeDamage(_attackData.Damage,closestPoint);
                }
            }
        }
        OnHit?.Invoke();
    }

    void SetEffects(Vector2 closestPoint){
        foreach (Transform fx in fxsOnImpact){
            fx.position = closestPoint;
        }
    }
}
