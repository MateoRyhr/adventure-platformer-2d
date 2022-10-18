using UnityEngine;

public class EntityAirJumpImpulse2D : EntityJumpImpulse2D
{
    private bool _jumpIsActive = false;

    void Update()
    {
        if(entityStatus.IsOnGround()) _jumpIsActive = true;
    }

    public override void PerformJump()
    {
        _jumpIsActive = false;
        rigidBody.AddForce(rigidBody.transform.up * JumpForce.Value,ForceMode2D.Impulse);
    }

    public override bool CanJump() => !entityStatus.IsOnGround() && _jumpIsActive;
}
