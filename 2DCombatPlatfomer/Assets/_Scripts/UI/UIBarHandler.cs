using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBarHandler : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Slider _slider;
    [Header("Config")]
    [SerializeField] FloatVariable _timePerUnit;
    [SerializeField] FloatVariable _maxValue;
    [SerializeField] AnimationCurve animationCurve;

    public UnityEvent BarComplete;
    public UnityEvent BarMovementFinished;

    private void Awake()
    {
        _slider.maxValue = _maxValue.Value;
    }

    public void StartMove(float newValue){
        float time = _timePerUnit.Value * Mathf.Abs(_slider.value - newValue);
        this.LerpFloat(
            _slider.value,
            newValue,
            time,
            MoveBar,
            false,
            animationCurve
        );
    }

    void MoveBar(float value){
        _slider.value = value;
        if(_slider.value <= 0) _slider.value = 0f;
    }

    public void ResetBar(){
        _slider.value = 0;
    }

    public void SetBar(float value){
        _slider.value = value;
    }
} 
