using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInfiniteBarHandler : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Slider _slider;
    [SerializeField] FloatVariable _currentValue;
    [Header("Config")]
    [SerializeField] FloatVariable _timePerUnit;
    [SerializeField] FloatVariable _maxValue;
    [SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0,0,1,1);

    private int _barNumber = 0;

    public UnityEvent BarMovementStart;
    public UnityEvent BarComplete;
    public UnityEvent BarMovementFinished;

    public void StartMove(){
        BarMovementStart?.Invoke();
        _barNumber = 0;
        float time = _timePerUnit.Value * Mathf.Abs(_currentValue.Value - _slider.value);
        this.LerpFloat(
            _slider.value,
            _currentValue.Value,
            time,
            MoveBar,
            false,
            animationCurve
        );
    }

    void MoveBar(float value){
        if(value > _maxValue.Value * (_barNumber+1)){
            _barNumber++;
            _currentValue.Value -= _maxValue.Value;
            BarComplete?.Invoke();
        } 
        _slider.value = value - _maxValue.Value * _barNumber;
        if(_slider.value + _maxValue.Value * _barNumber == _currentValue.Value){
            BarMovementFinished?.Invoke();
        }
    }

    public void ResetBar(){
        _currentValue.Value = 0;
        _slider.value = 0;
    }
} 
