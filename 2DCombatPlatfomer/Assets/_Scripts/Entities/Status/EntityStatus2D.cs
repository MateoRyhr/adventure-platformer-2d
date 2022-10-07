using UnityEngine;
using UnityEngine.Events;

public class EntityStatus2D : MonoBehaviour
{
    [Tooltip("The layer assigned to the ground - Needed to check if is on the ground")]
    [SerializeField] private LayerMask groundLayer;
    [Header("Status events")]
    public UnityEvent OnGround;
    public UnityEvent OnAir;
    
    public bool IsBeingAffectedByAnExternalForce { get; set; }

    private CapsuleCollider2D _collider;
    private float _width;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _width = _collider.size.x;
    }

    public bool IsOnGround(){
        float rayLength = 0.01f; //Ray length is a very small amount to avoid doble jump if the button is pressed two times quickly
        Vector3 leftSidePosition = new Vector3(transform.position.x - _width/2,transform.position.y,transform.position.z);
        Vector3 rightSidePosition = new Vector3(transform.position.x + _width/2,transform.position.y,transform.position.z);
        bool leftFootCheck = RaycastEnhanced.Raycast(leftSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        bool rightFootCheck = RaycastEnhanced.Raycast(rightSidePosition,Vector2.down,rayLength,groundLayer.value,true);
        return leftFootCheck || rightFootCheck;
    }

    public void ApplyForce(float time){
        IsBeingAffectedByAnExternalForce = true;
        this.Invoke(() => IsBeingAffectedByAnExternalForce = false, time);
    }
}
