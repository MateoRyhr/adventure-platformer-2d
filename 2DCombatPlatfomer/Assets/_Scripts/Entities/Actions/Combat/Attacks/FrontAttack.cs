using UnityEngine;

public class FrontAttack : Attack
{
    [Tooltip("Collider of the Attacker entity")]
    [SerializeField] private BoxCollider2D attackerCollider;
    [SerializeField] private int[] damageableLayers;
    [SerializeField] private ForceApplier2D forceApplier = null;
    [SerializeField] public Transform attackPoint;

    public override void PerformAttack()
    {
        Collider2D[] objectsImpacted = Physics2D.OverlapBoxAll(
            transform.position + Vector3.right * attackerCollider.transform.localScale.x * attackData.Range/2,
            new Vector2(attackData.Range,attackerCollider.size.y),0);
            
        foreach (var objectImpacted in objectsImpacted)
        {
            foreach (int damageableLayer in damageableLayers)
            {
                if(objectImpacted.gameObject.layer == damageableLayer){
                    IDamageTaker damageTaker = objectImpacted.GetComponentInChildren<IDamageTaker>();
                    Rigidbody2D targetRigidbody = objectImpacted.GetComponent<Rigidbody2D>();
                    Vector2 closestPoint = objectImpacted.ClosestPoint(attackPoint.position);
                    SetEffects(closestPoint);
                    if(targetRigidbody && forceApplier){
                        EntityStatus2D targetStatus = objectImpacted.GetComponent<EntityStatus2D>();
                        ApplyForce(targetRigidbody,closestPoint);
                        if(targetStatus) targetStatus.ApplyForce();
                    }
                    if(damageTaker != null){
                        damageTaker.TakeDamage(attackData.Damage,closestPoint);
                    }
                    OnAttackConnected?.Invoke();
                }
            }
        }
    }

    public override bool CanAttack() =>
        entityStatus.IsOnGround() &&
        attackStatus == AttackStatus.notStarted &&
        !entityStatus.IsAttacking;

    public override bool WasCancelled() => !entityStatus.IsOnGround();

    void SetEffects(Vector2 closestPoint){
        foreach (Transform fx in fxsOnImpact){
            fx.position = closestPoint;
        }
    }

    void ApplyForce(Rigidbody2D targetRigidbody, Vector2 closestPoint){
        Vector2 direction = new Vector2(forceApplier.ForceDirection.x * attackerCollider.transform.localScale.x,forceApplier.ForceDirection.y);
        forceApplier.ApplyForce(targetRigidbody,direction);
    }

    //For debug
    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(
    //         transform.position + Vector3.right * attackerCollider.transform.localScale.x * attackData.Range/2,
    //         new Vector3(attackData.Range,attackerCollider.size.y,0)
    //     );
    // }
}
