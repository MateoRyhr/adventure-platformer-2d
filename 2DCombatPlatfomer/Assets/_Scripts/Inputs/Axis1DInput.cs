using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Axis1DInput : MonoBehaviour
{
    [Header("Input set")]
    [SerializeField] InputActionAsset playerActionsAsset;
    [SerializeField] int actionMapNumber;
    [SerializeField] int actionNumber;
    // [Tooltip("Axis x = 0 / y = 1 / z = 2")]
    // [SerializeField] [Range(0,2)] int axis;
    // [Header("Data")]
    // [SerializeField] Vector3Variable axisInputValue; //Here will be written the input data
    [Header("Input events")]
    public UnityEvent OnAxisStart;
    public UnityEvent OnAxisMoving;
    public UnityEvent OnAxisEnd;

    //Values to determinate in which axis will be there the input value
    // private int _axisX = 0;
    // private int _axisY = 0;
    // private int _axisZ = 0;

    public float AxisValue { get; set; }
    // private float _axisValue = 0f;

    // private void Awake()
    // {   
    //     if(axis == 0) _axisX = 1;
    //     else if(axis == 1) _axisY = 1;
    //     else _axisZ = 1;
    // }

    private void Update() {
        AxisValue = playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].ReadValue<float>();
        // axisInputValue.Value = new Vector3(_axisValue * _axisX,_axisValue * _axisY,_axisValue * _axisZ);
        if(playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].IsPressed()){
            AxisMoving();
        }
    }

    public void AxisStart(InputAction.CallbackContext obj){
        if(playerActionsAsset.enabled) OnAxisStart?.Invoke();
    }
    
    public void AxisMoving(){
        if(playerActionsAsset.enabled) OnAxisMoving?.Invoke();
    }

    public void AxisEnd(InputAction.CallbackContext obj){
        if(playerActionsAsset.enabled) OnAxisEnd?.Invoke();
    }

    public void SubscribeAction(){
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].started += AxisStart;
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].performed += AxisEnd;
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].canceled += AxisEnd;
    }
    public void UnsubscribeAction(){
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].started -= AxisStart;
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].performed -= AxisEnd;
        playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].canceled -= AxisEnd;
    }

    private void OnEnable() {
        SubscribeAction();
    }
}
