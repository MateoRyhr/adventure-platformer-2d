using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateIdle : IState
{
    private EntityMovementController2D _movement;

    public StateIdle(EntityMovementController2D movementController){
        _movement = movementController;
    }

    public void OnEnter()
    {
        _movement.Direction = Vector2.zero;
    }

    public void Tick(){}
    public void OnExit(){}
}
