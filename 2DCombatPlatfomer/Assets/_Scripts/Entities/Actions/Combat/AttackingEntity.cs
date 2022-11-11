using UnityEngine;

public class AttackingEntity : MonoBehaviour
{
    [SerializeField] private EntityStatus2D _entityStatus;
    public EntityStatus2D EntityStatus { get => _entityStatus; }
    [SerializeField] private int[] _damageableLayers;
    public int[] DamageableLayers { get => _damageableLayers; }
    [SerializeField] private Animator _animator;
    [SerializeField] private string _cancelAttackAnimationTriggerName = "cancelAttack";
    public Animator Animator { get => _animator; }
    [SerializeField] private BoxCollider2D _collider;
    public BoxCollider2D Collider { get => _collider; }

    public void CancelAttackAnimation(){
        _animator.SetTrigger(_cancelAttackAnimationTriggerName);
    }
}
