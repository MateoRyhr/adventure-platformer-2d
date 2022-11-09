using System;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{

    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private EntityMovementController2D _movementControl;
    [SerializeField] private AIEntityData2D _aiData;
    [SerializeField] private EntityHealth _entityHealth;
    [SerializeField] private bool _onStartStayIdle;
    [SerializeField] private bool _lookAtDamageSourceOnDamage;

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

        if(_entityHealth && _lookAtDamageSourceOnDamage) _entityHealth.OnDamage.AddListener(LookAtDamageSource);

        _stateMachine.SetState(idle);

    }

    void LookAtDamageSource(){
        _movementControl.Direction = 
            _entityHealth.DamagePoint.x - _collider.transform.position.x < 0f
            ? Vector2.left
            : Vector2.right;
    }

    void Update() => _stateMachine.Tick();

    void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
}
