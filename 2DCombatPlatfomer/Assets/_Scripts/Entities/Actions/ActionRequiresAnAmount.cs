using UnityEngine;
using UnityEngine.Events;

public class ActionRequiresAnAmount : MonoBehaviour
{
    [SerializeField] private AmountInABar _amount;
    [SerializeField] private FloatVariable _cost;

    public UnityEvent OnAmountEnough;
    public UnityEvent OnAmountNotEnough;

    public void TryAction(){
        if(_amount.Value >= _cost.Value){
            _amount.Substract(_cost.Value);
            OnAmountEnough?.Invoke();
        }
        else
            OnAmountNotEnough?.Invoke();
    }
}
