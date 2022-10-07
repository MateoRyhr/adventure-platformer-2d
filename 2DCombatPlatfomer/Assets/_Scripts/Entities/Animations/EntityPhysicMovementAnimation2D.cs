using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPhysicMovementAnimation2D : MonoBehaviour
{
    Animator animator;
    [SerializeField] FloatVariable maxMovementSpeed;
    [SerializeField] Rigidbody2D rigidBody;

    const string VEL_X = "velX";
    const string VEL_Y = "velY";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        animator.SetFloat(VEL_X,Mathf.Abs(rigidBody.velocity.x / maxMovementSpeed.Value));
        animator.SetFloat(VEL_Y,rigidBody.velocity.y);
    }
}
