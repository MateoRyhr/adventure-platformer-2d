using UnityEngine;

public class AIEntityData2D : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private FloatVariable _maxDistanceToFall;
    [SerializeField] private FloatVariable _jumpHeight;
    [SerializeField] private FloatVariable _timeBeforeAttack;
    [SerializeField] private FloatVariable _timeToLostEnemy;
    [SerializeField] private EntitySightEnemyDetector2D _sight;

    public EntitySightEnemyDetector2D Sight { get => _sight;}
    public BoxCollider2D Collider { get => _collider; }    
    public bool HasATarget { get; set; }
    public float TimeBeforeAttack { get => _timeBeforeAttack.Value; }
    public float TimeToLostEnemy { get => _timeToLostEnemy.Value; }

    private float _aSmallAmount = 0.1f;
    private float _aSmallAmountBigger = 0.15f;

    public bool IsGroundInFront() => Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.down,_aSmallAmountBigger,_groundLayer);

    public bool IsGroundInFront(out RaycastHit2D hit) => hit = Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.down,_aSmallAmountBigger,_groundLayer);

    public bool IsGroundInFrontToFall() => Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.down,_maxDistanceToFall.Value,_groundLayer);

    public bool IsGroundInFrontToFall(out RaycastHit2D hit) => hit = Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.down,_maxDistanceToFall.Value,_groundLayer);

    public bool IsWallInFront() => Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.right * _collider.transform.lossyScale.x,0.1f,_groundLayer);

    public bool IsWallInFront(out RaycastHit2D hit) => hit = Physics2D.Raycast(FowardPointOnTheFloor(),Vector2.right * _collider.transform.lossyScale.x,0.1f,_groundLayer);

    public bool IsGroundInMaxPointReachedOnJump() => Physics2D.Raycast(new Vector2(_collider.transform.position.x,MaxPointReachedOnJump().y),Vector2.right * _collider.transform.localScale.x,_collider.size.x,_groundLayer);

    public bool CanJumpOverTheForwardObject() =>
        !Physics2D.BoxCast(CenterOfBodyIfJump(),_collider.size,0,Vector2.up,0,_groundLayer) &&
        !IsGroundInMaxPointReachedOnJump();

    private Vector2 FowardPointOnTheFloor(){
        return new Vector2(
            _collider.transform.position.x + _collider.size.x/2 * _collider.transform.lossyScale.x,
            _collider.transform.position.y + _aSmallAmount  //From foots A small amount up to hit the ground on down.
        );
    }

    private Vector2 MaxPointReachedOnJump(){
        return new Vector2(
            _collider.transform.position.x + _collider.size.x * _collider.transform.lossyScale.x,
            _collider.transform.position.y + _jumpHeight.Value
        );
    }

    private Vector2 PointToJumpOver() => Physics2D.Raycast(MaxPointReachedOnJump(),Vector2.down,Mathf.Infinity,_groundLayer).point;

    //Where it will be the center point of the collider in the jump
    private Vector2 CenterOfBodyIfJump(){
        return new Vector2(
            PointToJumpOver().x,
            PointToJumpOver().y + _collider.size.y/2 + _aSmallAmount
        );
    }

    // private void OnDrawGizmos() {
    //     RaycastHit2D hit;
    //     //Draw ground check
    //     Gizmos.color = IsGroundInFront(out hit) ? Color.green : Color.red;
    //     Gizmos.DrawLine(FowardPointOnTheFloor(),FowardPointOnTheFloor() + Vector2.down * _aSmallAmountBigger);
    //     //Draw ground to fall check
    //     Gizmos.color = IsGroundInFrontToFall(out hit) ? Color.yellow : Color.magenta;
    //     Gizmos.DrawLine(FowardPointOnTheFloor(),hit.point);
    //     //Draw Jump check
    //     Gizmos.DrawSphere(new Vector2(_collider.transform.position.x,MaxPointReachedOnJump().y),.05f);
    //     Gizmos.DrawSphere(MaxPointReachedOnJump(),.1f);
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawLine(MaxPointReachedOnJump(),PointToJumpOver());
    //     Gizmos.color = CanJumpOverTheForwardObject() ? Color.green : Color.red;
    //     Gizmos.DrawWireCube(CenterOfBodyIfJump(),_collider.size);
    // }
}
