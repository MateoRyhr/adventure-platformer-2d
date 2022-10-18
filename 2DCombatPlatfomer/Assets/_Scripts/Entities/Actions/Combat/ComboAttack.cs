using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [SerializeField] private EntityStatus2D entityStatus;
    [SerializeField] private Attack[] attacks;

    private int _currentAttack = 0;

    public void Attack()
    {
        //If is in attack
        if(attacks[_currentAttack].attackStatus != AttackStatus.notStarted){
            //If when the attack finish the combo finish
            if(attacks[_currentAttack].nextEvent == AttackNextEvent.finish){
                //Add an attack when the current attack finish
                attacks[_currentAttack].OnAttackFinished.RemoveListener(FinishCombo);//Remove the combo reset
                attacks[_currentAttack].OnAttackFinished.AddListener(AddAttack);
                attacks[_currentAttack].nextEvent = AttackNextEvent.attack;
            } //if when the attack finish the combo continue
            else if(attacks[_currentAttack].nextEvent == AttackNextEvent.attack){ 
                //Add an attack when the next attack finish
                int nextAttack = _currentAttack+1 < attacks.Length ? _currentAttack+1 : 0;
                if(attacks[nextAttack].nextEvent == AttackNextEvent.finish){
                    attacks[nextAttack].OnAttackStarted.AddListener(() => {
                        attacks[nextAttack].OnAttackFinished.RemoveAllListeners();
                        attacks[nextAttack].OnAttackFinished.AddListener(AddAttack);
                        attacks[nextAttack].nextEvent = AttackNextEvent.attack;
                    });
                }
            }       
        }else{
            if(!entityStatus.IsAttacking)
                StartAttack(_currentAttack);
        }
    }

    void AddAttack(){
        _currentAttack = _currentAttack+1 < attacks.Length ? _currentAttack+1 : 0;
        StartAttack(_currentAttack);
    }

    void StartAttack(int attack){
        attacks[attack].StartAttack();
        attacks[attack].OnAttackFinished.AddListener(FinishCombo);
    }

    void FinishCombo(){
        _currentAttack = 0;
    }
}
