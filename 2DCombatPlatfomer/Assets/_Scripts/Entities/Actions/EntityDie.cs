using UnityEngine;
using UnityEngine.Events;

public class EntityDie : MonoBehaviour
{
    [SerializeField] private EntityStatus2D _entityStatus;
    [SerializeField] private GameObject _actions;
    [SerializeField] private int CorpseMask;

    public UnityEvent OnDie;

    public void Die(){
        _entityStatus.gameObject.layer = CorpseMask;
        _actions.SetActive(false);
        OnDie?.Invoke();
    }
}
