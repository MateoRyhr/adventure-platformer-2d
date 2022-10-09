using UnityEngine;

public abstract class DamageTaker : MonoBehaviour
{
    public abstract void TakeDamage(float damage, Vector2 contactPoint = default);
    // public abstract void TakeDamage(float damage, Vector2 contactPoint);
}
