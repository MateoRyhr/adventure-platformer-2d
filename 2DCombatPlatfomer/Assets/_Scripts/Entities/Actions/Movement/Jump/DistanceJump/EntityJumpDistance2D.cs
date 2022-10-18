using UnityEngine;

public abstract class EntityJumpDistance2D : EntityJump2D
{
    [SerializeField] public FloatVariable Distance;
    [SerializeField] public FloatVariable JumpTime;
    [SerializeField] public AnimationCurve AnimationCurve;

    public override void PerformJump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,0f);
        Lerper.LerpFloat(
            this,
            rigidBody.transform.position.y,
            rigidBody.transform.position.y+Distance.Value,
            JumpTime.Value,
            Move,
            true,
            AnimationCurve
        );
    }

    void Move(float positionY){
        rigidBody.MovePosition(new Vector2(
            rigidBody.position.x + rigidBody.velocity.x * Time.fixedDeltaTime,
            positionY
        ));
    }
}
