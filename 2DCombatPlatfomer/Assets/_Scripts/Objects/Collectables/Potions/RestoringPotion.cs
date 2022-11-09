using UnityEngine;

public abstract class RestoringPotion : Potion
{
    [SerializeField] public float AmountToRestore;
    public Amount EntityAmount { get; set; }
    
    public override void ApplyEffects(Collider2D collector)
    {
        if(!ValueExceedsMaxValue())
            EntityAmount.Add(AmountToRestore);
        else
            EntityAmount.Add(ValueToReachTheMaximum());
    }

    bool ValueExceedsMaxValue() => AmountToRestore + EntityAmount.Value > EntityAmount.MaxValue;

    float ValueToReachTheMaximum() => EntityAmount.MaxValue - EntityAmount.Value;
}
