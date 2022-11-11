using UnityEngine;

public class PhysicalExplosiveProjectile : PhysicalProjectile
{
    [SerializeField] private FloatVariable _explosionRange;

    private void Awake() {
        OnHit.AddListener(() => Destroy(gameObject));
    }

    public override Vector2 ImpactForceDirection(Vector2 closestPoint)
    {
        Vector2 explosionDirection = closestPoint - (Vector2)transform.position;
        return ForceApplier.ForceDirection + explosionDirection;
    }

    public override Collider2D[] ObjectsImpacted() => Physics2D.OverlapCircleAll(transform.position,_explosionRange.Value);
}
