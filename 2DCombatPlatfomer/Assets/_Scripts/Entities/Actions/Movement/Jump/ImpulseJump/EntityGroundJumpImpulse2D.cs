using UnityEngine;

public class EntityGroundJumpImpulse2D : EntityJumpImpulse2D
{
    public override bool CanJump() => entityStatus.IsOnGround();

    public override void PerformJump()
    {
        rigidBody.AddForce(rigidBody.transform.up * JumpForce.Value,ForceMode2D.Impulse);
    }
}
