using UnityEngine;

public class EntityGroundJumpVelocity2D : EntityJumpVelocity2D
{
    public override void PerformJump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,Velocity.Value);
    }

    public override bool CanJump() => entityStatus.IsOnGround();
}
