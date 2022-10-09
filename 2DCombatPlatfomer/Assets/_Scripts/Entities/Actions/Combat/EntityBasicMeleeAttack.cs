using UnityEngine;
using UnityEngine.Events;

public class EntityBasicMeleeAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackTrigger;
    [SerializeField] private EntityStatus2D status;

    public UnityEvent OnAttack;

    public void Attack(){
        if(status.IsOnGround()){
            attackTrigger.gameObject.SetActive(true);
            OnAttack?.Invoke();
        }
    }
}
