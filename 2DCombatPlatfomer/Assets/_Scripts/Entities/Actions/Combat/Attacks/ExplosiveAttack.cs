using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : Attack
{
    [Tooltip("Collider of the Attacker entity")]
    [SerializeField] private Transform explosionPoint;
    [SerializeField] private int[] damageableLayers;
    [SerializeField] private ForceApplier2D forceApplier = null;

    public override void PerformAttack()
    {
        Collider2D[] objectsImpacted = Physics2D.OverlapCircleAll(explosionPoint.position,attackData.Range);
        foreach (Collider2D objectImpacted in objectsImpacted)
        {
            foreach (int layer in damageableLayers)
            {
                if(objectImpacted.gameObject.layer == layer){
                    DamageTaker damageTaker = objectImpacted.GetComponent<DamageTaker>();
                    Rigidbody2D targetRigidbody = objectImpacted.GetComponent<Rigidbody2D>();
                    Vector2 closestPoint = objectImpacted.ClosestPoint(explosionPoint.position);
                    SetEffects(closestPoint);
                    if(targetRigidbody && forceApplier){
                        ApplyForce(targetRigidbody,closestPoint);
                    }
                    if(damageTaker){
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
        Vector2 direction = (closestPoint - (Vector2)explosionPoint.position) + forceApplier.ForceDirection;
        forceApplier.ApplyForce(targetRigidbody,direction);
    }
}
