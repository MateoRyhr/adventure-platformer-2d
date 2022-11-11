using UnityEngine;

public class ExplosiveAttack : SimpleAttack
{
    public override Collider2D[] ObjectsImpacted() => Physics2D.OverlapCircleAll(AttackPoint.position,attackData.Range);

    public override Vector2 AttackForceDirection(Vector2 closestPoint)
    {
        Vector2 explosionDirection = closestPoint - (Vector2)AttackPoint.position;
        return ForceApplier.ForceDirection + explosionDirection;
    }
}
