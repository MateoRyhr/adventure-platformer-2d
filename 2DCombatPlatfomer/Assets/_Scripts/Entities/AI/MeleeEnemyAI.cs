using System;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemyAI : MonoBehaviour
{

    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private EntityMovementController2D _movementControl;
    [SerializeField] private AIEntityData2D _aiData;
    [SerializeField] private bool _onStartStayIdle;

    [SerializeField] private float _minActionTime;
    [SerializeField] private float _maxActionTime;

    [SerializeField] private Attack _attack;
    [SerializeField] private EntityJump2D _jump;

    private StateMachine _stateMachine;

    void Awake()
    {
        _stateMachine = new StateMachine();

        IState idle = new StateIdle(_movementControl);
        IState randomPatrolling = new StateRandomPatrolling(_movementControl,_aiData,_minActionTime,_maxActionTime);
        IState meleeCombat = new StateMeleeCombat(_aiData,_movementControl,_attack,() => _jump.Jump());

        At(idle,randomPatrolling,() => _onStartStayIdle == false);
        At(meleeCombat,randomPatrolling, () => !_aiData.HasATarget);
        _stateMachine.AddAnyTransition(meleeCombat,() => _aiData.Sight.EnemyInSight);

        _stateMachine.SetState(idle);

    }

    void Update() => _stateMachine.Tick();

    void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
}
