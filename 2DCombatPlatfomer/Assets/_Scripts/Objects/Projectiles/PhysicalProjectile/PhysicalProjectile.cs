using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalProjectile : Projectile
{
    [SerializeField] private FloatVariable _velocity;
    public float Velocity { get => _velocity.Value; }
        
    public override void Impulse(Vector2 direction)
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * Velocity;
    }

    private void OnCollisionEnter2D(Collision2D other){
        Hit();
    }
}
