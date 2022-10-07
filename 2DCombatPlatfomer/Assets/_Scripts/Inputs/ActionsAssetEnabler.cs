using UnityEngine.InputSystem;
using UnityEngine;

public class ActionsAssetEnabler : MonoBehaviour
{
    public InputActionAsset ActionsAsset;

    private void Awake()
    {
        EnableInputs();
    }

    public void EnableInputs(){
        Debug.Log("Inputs enabled");
        ActionsAsset.Enable();
    }

    public void DisableInputs(){
        Debug.Log("Inputs disabled");
        ActionsAsset.Disable();
    }
}
