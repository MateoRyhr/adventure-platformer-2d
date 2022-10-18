using UnityEngine;
// using UnityEngine.Events;

public class EntityStatus2D : MonoBehaviour
{
    [Tooltip("The layer assigned to the ground - Needed to check if is on the ground")]
    [SerializeField] private LayerMask groundLayer;
    // [Header("Status events")]
    // public UnityEvent OnGround;
    // public UnityEvent OnAir;
    
    public bool IsBeingAffectedByAnExternalForce { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsMoving { get; set; }

    private BoxCollider2D _collider;
    private float _width;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _width = _collider.size.x;
    }

    public bool IsOnGround(){
        float rayLength = 0.02f; //Ray length is a very small amount to avoid doble jump if the button is pressed two times quickly
        Vector3 leftSidePosition = new Vector3(transform.position.x - _width/2,transform.position.y,transform.position.z);
        Vector3 rightSidePosition = new Vector3(transform.position.x + _width/2,transform.position.y,transform.position.z);
        bool leftFootCheck = RaycastEnhanced.Raycast(leftSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        bool rightFootCheck = RaycastEnhanced.Raycast(rightSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        return leftFootCheck || rightFootCheck;
    }

    public bool CanMove(){
        return !IsAttacking && !IsBeingAffectedByAnExternalForce;
    }

    public void ApplyForce(float time){
        IsBeingAffectedByAnExternalForce = true;
        this.Invoke(() => IsBeingAffectedByAnExternalForce = false, time);
    }
}
