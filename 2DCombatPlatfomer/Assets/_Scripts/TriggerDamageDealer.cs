using UnityEngine;

public class TriggerDamageDealer : DamageDealer
{
    [SerializeField] private int[] damageableLayers;
    [SerializeField] private bool desactiveOnDamage;

    private BoxCollider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(DamageableTarget(collider)){
            Debug.Log("Trigger hit (ENTER)");
            DamageTaker damageTaker = collider.GetComponent<DamageTaker>();
            if(damageTaker) damageTaker.TakeDamage(Damage.Value);
            if(desactiveOnDamage) gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(DamageableTarget(collider)){
            Debug.Log("Trigger hit (STAY)");
            DamageTaker damageTaker = collider.GetComponent<DamageTaker>();
            if(damageTaker) damageTaker.TakeDamage(Damage.Value);
            if(desactiveOnDamage) gameObject.SetActive(false);
        }
    }

    bool DamageableTarget(Collider2D target)
    {
        foreach (int damageableLayer in damageableLayers){
            if(target.gameObject.layer == damageableLayer) return true;
        }
        return false;
    }
}
