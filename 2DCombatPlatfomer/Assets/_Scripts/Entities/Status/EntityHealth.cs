using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : AmountInABar, IDamageTaker
{
    [Header("Optional")]
    [SerializeField] private Transform _damageEffectPosition;
    public Vector2 DamagePoint { get; set; }
    private bool _canTakeDamage;
    public UnityEvent OnDamage;
    public UnityEvent OnDestruction;

    private void Start()
    {
        Initialize();
        _canTakeDamage = true;
        Bar.SetBar(Value);
        if(_damageEffectPosition) OnSubstractToTheAmount.AddListener(() => _damageEffectPosition.position = DamagePoint);
    }

    public void TakeDamage(float damage, Vector2 contactPoint = default)
    {
        if(_canTakeDamage){
            _canTakeDamage = false;
            DamagePoint = contactPoint;
            Substract(damage);
            OnDamage?.Invoke();
            if(Value <= 0) OnDestruction?.Invoke();
            this.Invoke(() => _canTakeDamage = true,Time.fixedDeltaTime);
        }
    }
}
