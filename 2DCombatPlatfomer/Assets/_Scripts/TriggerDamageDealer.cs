using UnityEngine;

public class TriggerDamageDealer : DamageDealer
{
    [SerializeField] private LayerMask[] damageableLayers;
    [SerializeField] private bool desactiveOnDamage;

    private BoxCollider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(DamageableTarget(collider)){
            collider.GetComponent<DamageTaker>().TakeDamage(Damage.Value);
            if(desactiveOnDamage) gameObject.SetActive(false);
        }
    }

    bool DamageableTarget(Collider2D target)
    {
        foreach (LayerMask damageableLayer in damageableLayers){
            if(target.gameObject.layer == damageableLayer.value) return true;
        }
        return false;
    }
}
