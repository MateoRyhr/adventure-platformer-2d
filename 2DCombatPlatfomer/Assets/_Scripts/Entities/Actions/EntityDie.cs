using UnityEngine;
using UnityEngine.Events;

public class EntityDie : MonoBehaviour
{
    [SerializeField] private EntityStatus2D _entityStatus;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _actions;
    [SerializeField] private int _corpseMask;
    [SerializeField] private PhysicsMaterial2D corpsePhysicMaterial;

    public UnityEvent OnDie;

    public void Die(){
        _entityStatus.gameObject.layer = _corpseMask;
        _collider.sharedMaterial = corpsePhysicMaterial;
        _collider.attachedRigidbody.sharedMaterial = corpsePhysicMaterial;
        _actions.SetActive(false);
        OnDie?.Invoke();
    }
}
