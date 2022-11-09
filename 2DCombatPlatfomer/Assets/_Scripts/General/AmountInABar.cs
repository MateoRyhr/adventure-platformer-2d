using UnityEngine;

public class AmountInABar : Amount
{
    [SerializeField] public UIBarHandler Bar;

    private void Awake() {
        OnAmountModified.AddListener(() => Bar.StartMove(Value));
    }
}
