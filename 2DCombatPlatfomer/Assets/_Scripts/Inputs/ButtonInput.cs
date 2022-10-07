using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonInput : MonoBehaviour
{
    public InputActionAsset PlayerActionsAsset;
    public int ActionMapNumber;
    public int ActionNumber;
    public UnityEvent Action;

    public void ButtonAction(InputAction.CallbackContext obj){
        Action.Invoke();
    }
    
    public void SubscribeAction(){
        PlayerActionsAsset.actionMaps[ActionMapNumber].actions[ActionNumber].started += ButtonAction;
    }
    public void UnsubscribeAction(){
        PlayerActionsAsset.actionMaps[ActionMapNumber].actions[ActionNumber].started -= ButtonAction;
    }

    private void OnEnable() {
        SubscribeAction();
    }
}
