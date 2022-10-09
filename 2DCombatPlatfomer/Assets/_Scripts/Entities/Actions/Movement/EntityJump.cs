using UnityEngine;
using UnityEngine.Events;

public class EntityJump : MonoBehaviour
{
    [SerializeField] private EntityStatus2D unitStatus;
    [SerializeField] private FloatVariable JumpForce;
    [SerializeField] private Rigidbody2D rigidBody;

    public UnityEvent JumpStart;

    public void Jump()
    {
        if(unitStatus.IsOnGround()){
            rigidBody.AddForce(Vector3.up * JumpForce.Value,ForceMode2D.Impulse);
            JumpStart?.Invoke();
        }
    }
}
