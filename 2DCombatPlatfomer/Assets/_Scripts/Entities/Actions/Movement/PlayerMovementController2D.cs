using UnityEngine;

public class PlayerMovementController2D : EntityMovementController2D
{
    [SerializeField] private Axis1DInput axisXInput;
    // [SerializeField] private Axis1DInput axisYInput;

    // private float xInput = 0f;
    // private float yInput = 0f;

    private void Update()
    {   
        // if(axisXInput) xInput = axisXInput.AxisValue;
        // if(axisYInput) yInput = axisYInput.AxisValue;
        Direction = new Vector2(axisXInput.AxisValue,0f);
    }
}
