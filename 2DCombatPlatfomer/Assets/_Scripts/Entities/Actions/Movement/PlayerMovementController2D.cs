using UnityEngine;

public class PlayerMovementController2D : EntityMovementController2D
{
    [SerializeField] private Axis1DInput axisXInput;
    [SerializeField] private Axis1DInput axisYInput;

    private void Update()
    {   
        Direction = new Vector2(axisXInput.AxisValue,axisYInput.AxisValue);
    }
}
