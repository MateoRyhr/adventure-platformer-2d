using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : DamageTaker
{
    [SerializeField] private FloatVariable maxHealth;
    [SerializeField] private bool hasStartingHealth;
    [SerializeField] private float startingHealth;
    [Header("Optional")]
    [SerializeField] Transform damageEffectPosition;
    [SerializeField] UIBarHandler _healthBar;
    private float _health;
    private bool _canTakeDamage;

    public UnityEvent OnDamage;
    public UnityEvent OnDestruction;

    private void Start()
    {
        if(hasStartingHealth) _health = startingHealth;
        else _health = maxHealth.Value;
        _canTakeDamage = true;
        if(_healthBar){
            _healthBar.SetBar(_health);
            OnDamage.AddListener(() => _healthBar.StartMove(_health));
        }
    }

    public override void TakeDamage(float damage, Vector2 contactPoint = default)
    {
        if(_canTakeDamage){
            _canTakeDamage = false;
            if(damageEffectPosition)
                damageEffectPosition.position = contactPoint;
            _health -= damage;
            OnDamage?.Invoke();
            if(_health <= 0) OnDestruction?.Invoke();
            this.Invoke(() => _canTakeDamage = true,Time.fixedDeltaTime);
        }
    }
}
