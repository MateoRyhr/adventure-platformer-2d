using UnityEngine;
using UnityEngine.Events;

public abstract class Amount : MonoBehaviour
{
    [SerializeField] private FloatVariable _maxValue;
    [Tooltip("If has a starting Value, the entity will start with Value = StartingValue, otherwise will use MaxValue")]
    [SerializeField] private bool _hasAStartingValue;
    [SerializeField] private float _startingValue;

    public float Value { get; set; }
    public float MaxValue { get => _maxValue.Value; }

    public UnityEvent OnAddToTheAmount;
    public UnityEvent OnSubstractToTheAmount;
    public UnityEvent OnAmountModified;

    public void Add(float value){
        Value += value;
        OnAddToTheAmount?.Invoke();
        OnAmountModified?.Invoke();
    }

    public void Substract(float value){
        Value -= value;
        OnSubstractToTheAmount?.Invoke();
        OnAmountModified?.Invoke();
    }

    public void SetAmount(float value){
        Value = value;
        OnAmountModified?.Invoke();
    }

    public void Initialize(){
        if(_hasAStartingValue) Value = _startingValue;
        else Value = MaxValue;
    }
}
