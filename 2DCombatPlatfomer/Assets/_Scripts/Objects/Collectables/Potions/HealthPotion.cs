using UnityEngine;

public class HealthPotion : RestoringPotion
{
    public override bool CanApplyEffects(Collider2D collector)
    {
        EntityHealth entityHealth = collector.GetComponentInChildren<EntityHealth>();;
        EntityAmount = entityHealth;
        return EntityAmount != null && EntityAmount.Value < EntityAmount.MaxValue;
    }
}
