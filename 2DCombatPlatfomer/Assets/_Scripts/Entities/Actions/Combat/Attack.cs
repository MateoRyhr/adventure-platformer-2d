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
    [SerializeField] public AttackData attackData;
    [SerializeField] public AttackingEntity attackingEntity;
    [Tooltip("The name of the trigger that start the attack animation")]
    public string _attackAnimationName;
    [HideInInspector] public AttackStatus attackStatus = AttackStatus.notStarted;
    [HideInInspector] public AttackNextEvent nextEvent = AttackNextEvent.finish;

    public UnityEvent OnAttackStarted;
    public UnityEvent OnAttackPerformed;
    public UnityEvent OnAttackConnected;
    public UnityEvent OnAttackFinished;
    public UnityEvent OnAttackCancelled;

    private void Update() {
        if(attackStatus != AttackStatus.notStarted && WasCancelled()) CancelAttack();
    }

    public void StartAttack(){
        if(CanAttack()){
            OnAttackStarted?.Invoke();
            attackingEntity.EntityStatus.IsAttacking = true;
            nextEvent = AttackNextEvent.finish;
            attackStatus = AttackStatus.started;
            PlayAttackAnimation();
            this.Invoke( () => {
                if(!WasCancelled()){
                    PerformAttack();
                    attackStatus = AttackStatus.performed;
                    OnAttackPerformed?.Invoke();
                }
            }, attackData.TimeUntilAttackDone);

            this.Invoke(() => {
                attackingEntity.EntityStatus.IsAttacking = false;
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
        RestartAttack();
        OnAttackCancelled?.Invoke();
        attackingEntity.CancelAttackAnimation();
    }

    public void RestartAttack(){
        attackingEntity.EntityStatus.IsAttacking = false;
        nextEvent = AttackNextEvent.finish;
        CancelInvoke();
        FinishAttack();
    }

    void PlayAttackAnimation(){
        attackingEntity.Animator.SetTrigger(_attackAnimationName);
    }

    public abstract void PerformAttack();
    public abstract bool WasCancelled();
    public abstract bool CanAttack();
}
