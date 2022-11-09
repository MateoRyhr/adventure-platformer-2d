using UnityEngine;

public interface IDamageTaker
{
    public abstract void TakeDamage(float damage, Vector2 contactPoint = default);
}
