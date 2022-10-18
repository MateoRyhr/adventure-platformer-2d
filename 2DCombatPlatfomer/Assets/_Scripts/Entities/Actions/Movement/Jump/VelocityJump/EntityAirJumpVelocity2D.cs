using UnityEngine;

public class EntityAirJumpVelocity2D : EntityJumpVelocity2D
{
    private bool _jumpIsActive = false;

    void Update()
    {
        if(entityStatus.IsOnGround()) _jumpIsActive = true;
    }

    public override void PerformJump()
    {
        _jumpIsActive = false;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,Velocity.Value);
    }

    public override bool CanJump() => !entityStatus.IsOnGround() && _jumpIsActive;
}
