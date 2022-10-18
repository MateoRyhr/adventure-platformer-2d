using UnityEngine;
using UnityEngine.Events;

public enum AttackStatus
{
    notStarted,
    started,
    performed
}

public enum AttackNextEvent
{
    finish,
    attack
}

public abstract class Attack : MonoBehaviour
{
    public AttackData attackData;
    public EntityStatus2D entityStatus;
    [HideInInspector] public AttackStatus attackStatus = AttackStatus.notStarted;
    [HideInInspector] public AttackNextEvent nextEvent = AttackNextEvent.finish;
    [Header("Optional")]
    // [SerializeField] public Transform[] fxsOnStart;
    // [SerializeField] public Transform[] fxsOnPerform;
    [SerializeField] public Transform[] fxsOnImpact;
    // [SerializeField] private LayerMask damageableLayers; --> All attacks need a Layermask? Possible abstraction
    public UnityEvent OnAttackStarted;
    public UnityEvent OnAttackPerformed;
    public UnityEvent OnAttackConnected;
    public UnityEvent OnAttackFinished;

    private void Update() {
        if(WasCancelled()) CancelAttack();
    }

    public void StartAttack(){
        if(CanAttack()){
            entityStatus.IsAttacking = true;
            nextEvent = AttackNextEvent.finish;
            attackStatus = AttackStatus.started;
            OnAttackStarted?.Invoke();

            this.Invoke( () => {
                PerformAttack();
                attackStatus = AttackStatus.performed;
                OnAttackPerformed?.Invoke();
            }, attackData.TimeUntilAttackDone);

            this.Invoke(() => {
                entityStatus.IsAttacking = false;
                OnAttackFinished?.Invoke();
                FinishAttack();              
            }, attackData.AnimationTime);
        }
    }

    public void FinishAttack(){
        attackStatus = AttackStatus.notStarted;
        nextEvent = AttackNextEvent.finish;
        OnAttackStarted.RemoveAllListeners();
        OnAttackFinished.RemoveAllListeners();
    }

    public void CancelAttack(){
        entityStatus.IsAttacking = false;
        nextEvent = AttackNextEvent.finish;
        CancelInvoke();
        FinishAttack();
    }

    public abstract void PerformAttack();
    public abstract bool WasCancelled();
    public abstract bool CanAttack();
}
