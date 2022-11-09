using UnityEngine;
using UnityEngine.Events;

public class EntityTeleport2D : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private EntityMovementController2D _control;
    [SerializeField] public LayerMask _groundLayer;
    [Tooltip("A higher number gives a more accurate teleport but have less performance. A meter will be divided by the number. For example you put 2 so 1 / 2 = 0.5 per .5 meter a teleport will be tried.")]
    [SerializeField] private float _precision = 10;
    [Tooltip("Amount of time to wait after use it")]
    [SerializeField] public FloatVariable Distance;
    [SerializeField] public FloatVariable TimeWhenTeleportIsDone;
    [SerializeField] public FloatVariable AnimationTime;

    public UnityEvent OnTeleportStart;
    public UnityEvent OnTeleportPerformedBefore;
    public UnityEvent OnTeleportPerformedAfter;
    public UnityEvent OnTeleportEnd;

    private int _points;
    private float _distanceToMovePerTry;

    private void Awake()
    {
        _distanceToMovePerTry = 1f / _precision;
        _points = (int)(Distance.Value / _distanceToMovePerTry);
    }

    public void Teleport()
    {
        OnTeleportStart?.Invoke();

        this.Invoke(() => {
            OnTeleportPerformedBefore?.Invoke();
            _rigidBody.transform.SetPositionAndRotation(TeleportPosition(),_rigidBody.transform.rotation);// _rigidBody.MovePosition(TeleportPosition()); --> can't pass through objects
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,0f);
        },TimeWhenTeleportIsDone.Value);

        this.Invoke(() => OnTeleportPerformedAfter?.Invoke(),TimeWhenTeleportIsDone.Value + Time.deltaTime);

        this.Invoke(() => {
            OnTeleportEnd?.Invoke();
        },AnimationTime.Value);
    }

    Vector2 TeleportPosition()
    {
        //before --> CanTeleportAtMaxDistance() ? MaxDistancePoint() : PointWhenTeleportCanBeReleased();
        //now --> Vamos a hacer un boxcast n veces empezando desde la maxima distancia hasta la posicion actual, hasta que uno de los boxCast no colisione
        for(int i = 0; i < _points; i++){
            Vector2 point = MaxDistancePoint() - TeleportDirection() * (i*_distanceToMovePerTry);
            if(CanTeleportAtPoint(point))
                return point;
        }
        return _rigidBody.transform.position;
    }

    private bool CanTeleportAtPoint(Vector2 point) => !Physics2D.OverlapBox(point + Vector2.up * (_collider.size.y/2),_collider.size,0,_groundLayer);

    //The teleport can't be diagonal, priorizes the input in axis y, otherwise use axis x.
    private Vector2 TeleportDirection()
    {
        if(_control.Direction.y != 0) return new Vector2(0,_control.Direction.y);
        else if(_control.Direction.x != 0) return new Vector2(_control.Direction.x,0);
        else return new Vector2(_rigidBody.transform.lossyScale.x,0);
    }

    private Vector2 MaxDistancePoint() => (Vector2) _rigidBody.transform.position+Vector2.up*.01f + TeleportDirection() * Distance.Value;

    // private void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawCube(TeleportPosition() + Vector2.up * (_collider.size.y/2),_collider.size);
    // }
}
