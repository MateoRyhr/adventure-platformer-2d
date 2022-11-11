using UnityEngine;

public class FrontAttack : SimpleAttack
{
    public override Collider2D[] ObjectsImpacted()
    {
        return Physics2D.OverlapBoxAll(
            transform.position + Vector3.right * attackingEntity.Collider.transform.localScale.x * attackData.Range/2,
            new Vector2(attackData.Range,attackingEntity.Collider.size.y),0);
    }

    public override Vector2 AttackForceDirection(Vector2 closestPoint) => 
        new Vector2(ForceApplier.ForceDirection.x * attackingEntity.Collider.transform.localScale.x,ForceApplier.ForceDirection.y);
        
    //For debug
    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(
    //         transform.position + Vector3.right * attackerCollider.transform.localScale.x * attackData.Range/2,
    //         new Vector3(attackData.Range,attackerCollider.size.y,0)
    //     );
    // }
}
