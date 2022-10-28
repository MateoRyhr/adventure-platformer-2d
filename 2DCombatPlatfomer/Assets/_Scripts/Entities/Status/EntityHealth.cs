using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : DamageTaker
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private FloatVariable _maxHealth;
    [Tooltip("If has a starting health, the entity will start with Health = StartingHealth, otherwise will use MaxHealth")]
    [SerializeField] private bool _hasStartingHealth;
    [SerializeField] private float _startingHealth;
    [Tooltip("If is enabled the entity will look at the damage source when gets damage")]
    [SerializeField] private bool _lookAtDamageSourceOnDamage;
    [Header("Optional")]
    [SerializeField] private Transform _damageEffectPosition;
    [SerializeField] private UIBarHandler _healthBar;

    public Vector2 DamagePoint { get; set; }

    private float _health;
    private bool _canTakeDamage;

    public UnityEvent OnDamage;
    public UnityEvent OnDestruction;

    private void Start()
    {
        _canTakeDamage = true;
        if(_hasStartingHealth) _health = _startingHealth;
        else _health = _maxHealth.Value;
        if(_healthBar){
            _healthBar.SetBar(_health);
            OnDamage.AddListener(() => _healthBar.StartMove(_health));
        }
        if(_damageEffectPosition) OnDamage.AddListener(() => _damageEffectPosition.position = DamagePoint);
    }

    public override void TakeDamage(float damage, Vector2 contactPoint = default)
    {
        if(_canTakeDamage){
            _canTakeDamage = false;
            DamagePoint = contactPoint;
            _health -= damage;
            OnDamage?.Invoke();
            if(_health <= 0) OnDestruction?.Invoke();
            this.Invoke(() => _canTakeDamage = true,Time.fixedDeltaTime);
        }
    }

    // void LookAtDamageSource(){
    //     _collider.transform.localScale = new Vector3(
    //         DamagePoint.x - _collider.transform.position.x < 0f ? -1 : 1,
    //         1,
    //         1
    //     );
    // }
}
