using UnityEngine;
using UnityEngine.Events;

public class EntityJump : MonoBehaviour
{
    [SerializeField] EntityStatus2D unitStatus;
    [SerializeField] FloatVariable JumpForce;
    Rigidbody2D rb;

    public UnityEvent JumpStart;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if(unitStatus.IsOnGround()){
            rb.AddForce(Vector3.up * JumpForce.Value,ForceMode2D.Impulse);
            JumpStart?.Invoke();
        }
    }
}
