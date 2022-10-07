using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Axis1DInput : MonoBehaviour
{
    [Header("Input set")]
    [SerializeField] InputActionAsset playerActionsAsset;
    [SerializeField] int actionMapNumber;
    [SerializeField] int actionNumber;
    [Header("Input events")]
    public UnityEvent OnAxisStart;
    public UnityEvent OnAxisMoving;
    public UnityEvent OnAxisEnd;

    public float AxisValue { get; set; }

    private void Update() {
        AxisValue = playerActionsAsset.actionMaps[actionMapNumber].actions[actionNumber].ReadValue<float>();
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
