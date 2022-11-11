using UnityEngine;
using UnityEngine.Events;

public class ActionCooldown : MonoBehaviour
{
    [SerializeField] private FloatVariable _cooldown;
    public bool Enabled { get; set; }

    public UnityEvent OnAction;
    public UnityEvent OnActionInCooldown;

    private void Start()
    {
        Enabled = true;
    }

    public void TryAction()
    {
        if(Enabled)
        {
            OnAction?.Invoke();
        }
        else
        {
            OnActionInCooldown?.Invoke();
        }
    }

    public void ActiveAfterCooldownTime() => this.Invoke(() => Enabled = true,_cooldown.Value);
}
