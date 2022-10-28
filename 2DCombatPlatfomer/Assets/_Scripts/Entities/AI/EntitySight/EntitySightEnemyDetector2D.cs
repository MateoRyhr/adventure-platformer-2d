using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySightEnemyDetector2D : MonoBehaviour
{
    public Collider2D EnemyInSight { get; set; }

    [SerializeField] private Collider2D _entityCollider;
    [SerializeField] private Transform _visionOriginPoint;
    [Tooltip("The amplitude of the field of view")]
    [SerializeField] private FloatVariable _visionAngle;
    [Tooltip("How far can see")]
    [SerializeField] private FloatVariable _visionRange;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstructionLayer;

    private Collider2D[] _rangeChecks;

    private void Update()
    {
        See();
    }

    private void See()
    {
        EnemyInSight = null;
        _rangeChecks = Physics2D.OverlapCircleAll(_visionOriginPoint.position,_visionRange.Value,_enemyLayer);
        if(_rangeChecks.Length == 0) return;

        Vector2 targetPointInSight = _rangeChecks[0].ClosestPoint(_visionOriginPoint.position);
        Vector2 directionToTarget = ((Vector3)targetPointInSight - _visionOriginPoint.position).normalized;
        Vector2 forward = _visionOriginPoint.right * _entityCollider.transform.lossyScale.x;

        if(Vector2.Angle(forward,directionToTarget) < _visionAngle.Value / 2){ //If the enemy is inside the field of view <) 
            float distanceToTarget = Vector2.Distance(_visionOriginPoint.position, targetPointInSight);
            if(!Physics2D.Raycast(_visionOriginPoint.position,directionToTarget,distanceToTarget,_obstructionLayer)){
                // Debug.Log("Enemy in sight");
                EnemyInSight = _rangeChecks[0];
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_visionOriginPoint.position,_visionRange.Value);
        // IsSeeAnEnemy();
        // Gizmos.DrawWireSphere(targetPointInSight,0.1f);
    }
}
