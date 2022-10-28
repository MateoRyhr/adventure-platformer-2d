using UnityEngine;

public class StateRandomPatrolling : IState
{
    private EntityMovementController2D _movement;
    private AIEntityData2D _aiData;
    private float _minTime;
    private float _maxTime;

    private float _timeUntilNextMovement;

    public StateRandomPatrolling(EntityMovementController2D movementController, AIEntityData2D aiData,float minTime, float maxTime){
        _movement = movementController;
        _aiData = aiData;
        _minTime = minTime;
        _maxTime = maxTime;
    }

    public void OnEnter(){}

    public void Tick()
    {
        if(_timeUntilNextMovement <= 0f || !_aiData.IsGroundInFront() || _aiData.IsWallInFront()){
            SetMovement();
            _timeUntilNextMovement = Random.Range(_minTime,_maxTime);
        }
        _timeUntilNextMovement -= Time.deltaTime;
    }

    public void OnExit(){}

    private void SetMovement(){
        float random = Random.Range(0f,1f);
        //If can't walk forward, stay quiet or move in the opposite direction
        if(!_aiData.IsGroundInFront() || _aiData.IsWallInFront()){
            _movement.Direction = random < .5f ? Vector2.zero : (Vector2.right * _aiData.Collider.transform.lossyScale.x) * -1;
        }else{//Otherwise pick random movement
            if(random < .33f) _movement.Direction = Vector2.zero;
            else _movement.Direction = random < .66f ? Vector2.left : Vector2.right;
        }
    }
}
