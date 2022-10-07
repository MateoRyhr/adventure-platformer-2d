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
        ActionsAsset.Enable();
    }

    public void DisableInputs(){
        ActionsAsset.Disable();
    }
}
