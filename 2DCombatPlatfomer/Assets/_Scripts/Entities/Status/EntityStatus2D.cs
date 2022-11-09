using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Events;

public class EntityStatus2D : MonoBehaviour
{
    [Tooltip("The layer assigned to the ground - Needed to check if is on the ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int _additionalPointsToCheckGround;
    // [Header("Status events")]
    // public UnityEvent OnGround;
    // public UnityEvent OnAir;
    
    public bool IsBeingAffectedByAnExternalForce { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsMoving { get; set; }

    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private float _width;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _width = _collider.size.x;
    }

    public bool IsOnGround(){
        float rayLength = 0.02f; //Ray length is a very small amount to avoid doble jump if the button is pressed two times quickly
        bool checkAtCenter = RaycastEnhanced.Raycast(transform.position,Vector2.down,rayLength,groundLayer.value,true);
        if(checkAtCenter) return true;
        // List<bool> checks = new List<bool>();
        // checks.Add(checkAtCenter);
        for(int i = 1; i <= _additionalPointsToCheckGround; i++){
            bool leftCheck = RaycastEnhanced.Raycast((Vector2)transform.position + Vector2.left * (_width/2/_additionalPointsToCheckGround*i),Vector2.down,rayLength,groundLayer.value,true);
            bool rightCheck = RaycastEnhanced.Raycast((Vector2)transform.position + Vector2.right * (_width/2/_additionalPointsToCheckGround*i),Vector2.down,rayLength,groundLayer.value,true);
            if(leftCheck || rightCheck) return true;
            // checks.Add(leftCheck);
            // checks.Add(rightCheck);
        }
        return false;
        // Vector3 leftSidePosition = new Vector3(transform.position.x - _width/2,transform.position.y,transform.position.z);
        // Vector3 rightSidePosition = new Vector3(transform.position.x + _width/2,transform.position.y,transform.position.z);
        // bool leftFootCheck = RaycastEnhanced.Raycast(leftSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        // bool rightFootCheck = RaycastEnhanced.Raycast(rightSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        // return leftFootCheck || rightFootCheck;
    }

    public bool CanMove(){
        return !IsAttacking && !IsBeingAffectedByAnExternalForce;
    }

    public void ApplyForce(){
        IsBeingAffectedByAnExternalForce = true;
        this.Invoke(() => IsBeingAffectedByAnExternalForce = false,0.5f);
    }
}
