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
        //If is in attack
        if(_attacks[_currentAttack].attackStatus != AttackStatus.notStarted){
            //If when the attack finish the combo finish
            if(_attacks[_currentAttack].nextEvent == AttackNextEvent.finish){
                //Add an attack when the current attack finish
                _attacks[_currentAttack].OnAttackFinished.RemoveListener(FinishCombo);//Remove the combo reset
                _attacks[_currentAttack].OnAttackFinished.AddListener(AddAttack);
                _attacks[_currentAttack].nextEvent = AttackNextEvent.attack;
            } //if when the attack finish the combo continue
            else if(_attacks[_currentAttack].nextEvent == AttackNextEvent.attack){ 
                //Add an attack when the next attack finish
                int nextAttack = _currentAttack+1 < _attacks.Length ? _currentAttack+1 : 0;
                if(_attacks[nextAttack].nextEvent == AttackNextEvent.finish){
                    _attacks[nextAttack].OnAttackStarted.AddListener(() => {
                        _attacks[nextAttack].OnAttackFinished.RemoveAllListeners();
                        _attacks[nextAttack].OnAttackFinished.AddListener(AddAttack);
                        _attacks[nextAttack].nextEvent = AttackNextEvent.attack;
                    });
                }
            }       
        }else{
            if(!_entityStatus.IsAttacking)
                StartAttack(_currentAttack);
        }
    }

    void AddAttack(){
        _currentAttack = _currentAttack+1 < _attacks.Length ? _currentAttack+1 : 0;
        StartAttack(_currentAttack);
    }

    void StartAttack(int attack){
        _attacks[attack].OnAttackFinished.AddListener(FinishCombo);
        _attacks[attack].StartAttack();
    }

    void FinishCombo(){
        _currentAttack = 0;
    }

    public void CancelCombo(){
        FinishCombo();
        foreach (Attack attack in _attacks)
        {
            attack.RestartAttack();
        }
    }
}
