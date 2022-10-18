using UnityEngine;
using UnityEngine.Events;

public abstract class EntityJump2D : MonoBehaviour
{
    [SerializeField] public EntityStatus2D entityStatus;
    [SerializeField] public Rigidbody2D rigidBody;

    public UnityEvent OnJumpStart;

    public void Jump()
    {
        if(CanJump()){
            PerformJump();
            OnJumpStart?.Invoke();
        }
    }

    public abstract void PerformJump();

    public abstract bool CanJump();
}
