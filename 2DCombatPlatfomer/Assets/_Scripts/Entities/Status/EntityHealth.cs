using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : DamageTaker
{
    [SerializeField] private FloatVariable maxHealth;
    [SerializeField] private bool hasStartingHealth;
    [SerializeField] private float startingHealth;
    [SerializeField] Transform damageEffectPosition;
    private float _health;
    private bool _canTakeDamage;

    public UnityEvent OnDamage;
    public UnityEvent OnDestruction;

    private void Start()
    {
        if(hasStartingHealth) _health = startingHealth;
        else _health = maxHealth.Value;
        _canTakeDamage = true;
    }

    public override void TakeDamage(float damage, Vector2 contactPoint = default)
    {
        if(_canTakeDamage){
            _canTakeDamage = false;
            if(damageEffectPosition)
                damageEffectPosition.position = contactPoint;
            OnDamage?.Invoke();
            _health -= damage;
            if(_health <= 0) OnDestruction?.Invoke();
            this.Invoke(() => _canTakeDamage = true,Time.fixedDeltaTime);
        }
    }
}
