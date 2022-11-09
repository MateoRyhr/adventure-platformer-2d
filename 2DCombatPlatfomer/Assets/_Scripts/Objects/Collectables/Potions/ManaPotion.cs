using UnityEngine;

public class ManaPotion : RestoringPotion
{
    public override bool CanApplyEffects(Collider2D collector)
    {
        EntityMana entityMana = collector.GetComponentInChildren<EntityMana>();
        EntityAmount = entityMana;
        return EntityAmount != null && EntityAmount.Value < EntityAmount.MaxValue;
    }
}
