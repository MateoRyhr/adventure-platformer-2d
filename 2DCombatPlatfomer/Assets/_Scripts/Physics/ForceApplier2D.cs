using UnityEngine;

public class ForceApplier2D : MonoBehaviour
{
    [SerializeField] public Vector2 ForceDirection;
    [SerializeField] private FloatVariable force;

    public void ApplyForce(Rigidbody2D rigidBody){
        rigidBody.AddForce(ForceDirection.normalized * force.Value,ForceMode2D.Impulse);
    }

    public void ApplyForce(Rigidbody2D rigidBody, Vector2 direction){
        rigidBody.AddForce(direction.normalized * force.Value,ForceMode2D.Impulse);
    }
}
