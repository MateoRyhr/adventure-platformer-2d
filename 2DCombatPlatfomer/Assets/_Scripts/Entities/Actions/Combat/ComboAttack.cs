using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [SerializeField] private EntityStatus2D _entityStatus;
    [SerializeField] private Attack[] _attacks;

    private int _currentAttack = 0;

    private void Awake() {
        foreach (Attack attack in _attacks)
        {
            attack.OnAttackCancelled.AddListener(() => CancelCombo());
        }
    }

    public void Attack()
    {
        if(!_entityStatus.IsAttacking && _currentAttack == 0)
        {
            StartAttack(_currentAttack);
            return;
        }
        //If is in attack
        if(_attacks[_currentAttack].attackStatus != AttackStatus.notStarted){
            int nextAttack = NextAttack(_currentAttack);
            //If when the attack finish the combo finish
            if(_attacks[_currentAttack].nextEvent == AttackNextEvent.finish){
                //Add an attack when the current attack finish
                _attacks[_currentAttack].OnAttackFinished.RemoveAllListeners();//Remove the combo reset
                _attacks[_currentAttack].OnAttackFinished.AddListener(() => AddAttack(nextAttack));
                _attacks[_currentAttack].nextEvent = AttackNextEvent.attack;
            } //if when the attack finish the combo continue
            else if(_attacks[_currentAttack].nextEvent == AttackNextEvent.attack){ 
                //Add an attack when the next attack finish
                if(_attacks[nextAttack].nextEvent == AttackNextEvent.finish){
                    _attacks[nextAttack].OnAttackStarted.AddListener(() => {
                        _attacks[nextAttack].OnAttackFinished.RemoveAllListeners();
                        _attacks[nextAttack].OnAttackFinished.AddListener(() => AddAttack(NextAttack(nextAttack)));
                        _attacks[nextAttack].nextEvent = AttackNextEvent.attack;
                    });
                }
            }       
        }
        // else
        // {
        //     if(!_entityStatus.IsAttacking)
        //         StartAttack(_currentAttack);
        // }
    }

    void AddAttack(int attack){
        _currentAttack = NextAttack(_currentAttack);
        StartAttack(attack);
    }

    void StartAttack(int attack){
        _attacks[attack].OnAttackFinished.AddListener(FinishCombo);
        _attacks[attack].StartAttack();
    }

    void FinishCombo(){
        _currentAttack = 0;
    }

    int NextAttack(int currentAttack) => currentAttack+1 < _attacks.Length ? currentAttack+1 : 0;

    public void CancelCombo(){
        FinishCombo();
        foreach (Attack attack in _attacks)
        {
            attack.RestartAttack();
        }
    }
}
