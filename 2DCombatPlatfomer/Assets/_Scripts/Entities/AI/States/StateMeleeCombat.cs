using System;
using UnityEngine;

public class StateMeleeCombat : IState
{
    private AIEntityData2D _aiData;
    private EntityMovementController2D _movement;

    private float _timeToLostEnemy;
    private float _timeUntilLostEnemy = 0f;

    private float _timeBeforeAttack;
    private Attack _attack;

    private Action PerformAttack;
    private Action Jump;

    private bool _enemyLastPositionReached = false;
    private Vector2 _enemyLastPosition;
    private Vector2 _enemyLastDirection;
    private float  _distanceToLastPosition;
    private float  _distanceToTarget;
    private float _timeUntilAttack = 0f;

    public StateMeleeCombat(AIEntityData2D data, EntityMovementController2D movement, Attack attack, Action OnJump){
        _aiData = data;
        _movement = movement;
        _attack = attack;
        _timeToLostEnemy = _aiData.TimeToLostEnemy;
        _timeBeforeAttack = _aiData.TimeBeforeAttack;
        PerformAttack = () => _attack.StartAttack();
        Jump = OnJump;
    }

    public void OnEnter(){}

    public void OnExit() {}

    public void Tick()
    {
        if(_aiData.Sight.EnemyInSight){
            _enemyLastPositionReached = false;
            _aiData.HasATarget = true;
            _enemyLastPosition = _aiData.Sight.EnemyInSight.transform.position;
            _enemyLastDirection = _aiData.Sight.EnemyInSight.attachedRigidbody.velocity.x > 0 ? Vector2.right : Vector2.left;
            _distanceToLastPosition = Mathf.Abs( _enemyLastPosition.x - _aiData.Collider.transform.position.x);
            _distanceToTarget = Mathf.Abs(_enemyLastPosition.x - _aiData.Collider.transform.position.x);
            _timeUntilLostEnemy = _timeToLostEnemy;
            if(_distanceToTarget < _attack.attackData.Range) Attack();
            else FollowEnemy();
        }else{
            _distanceToLastPosition = Mathf.Abs(_enemyLastPosition.x - _aiData.Collider.transform.position.x);
            FollowLastEnemyPosition();
            _timeUntilLostEnemy -= Time.deltaTime;
            if(_timeUntilLostEnemy <= 0f) _aiData.HasATarget = false;
        }
    }

    void FollowEnemy(){
        Vector2 direction =
            (_enemyLastPosition.x - _aiData.Collider.transform.position.x) < 0
            ? Vector2.left : Vector2.right;
        _movement.Direction = direction;
        if(!_aiData.IsGroundInFrontToFall()) _movement.Direction = Vector2.zero;
        if(_aiData.IsWallInFront())
            if(_aiData.CanJumpOverTheForwardObject())
                Jump.Invoke();
            else
                _movement.Direction = Vector2.zero;
    }

    void FollowLastEnemyPosition(){
        if(_distanceToLastPosition > 0.2f && !_enemyLastPositionReached){
            _movement.Direction = (_enemyLastPosition.x - _aiData.Collider.transform.position.x) > 0
                ? Vector2.right
                : Vector2.left;
        }else{
            _enemyLastPositionReached = true;
        }
        if(_enemyLastPositionReached) _movement.Direction = _enemyLastDirection;
        if(_aiData.IsWallInFront())
            if(_aiData.CanJumpOverTheForwardObject())
                Jump.Invoke();
            else
                _movement.Direction = Vector2.zero;
        if(!_aiData.IsGroundInFrontToFall()) _movement.Direction = Vector2.zero;
    }

    void Attack(){
        _movement.Direction = Vector2.zero;
        if(_timeUntilAttack <= 0f){
            PerformAttack.Invoke();
            _timeUntilAttack = _timeBeforeAttack + _attack.attackData.AnimationTime;
        }
        else{
            _timeUntilAttack -= Time.deltaTime;
        }
    }
}
