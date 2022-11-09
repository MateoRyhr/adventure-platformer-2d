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
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,0f);
        rigidBody.AddForce(rigidBody.transform.up * JumpForce.Value,ForceMode2D.Impulse);
    }

    public override bool CanJump() => !entityStatus.IsOnGround() && _jumpIsActive;
}
