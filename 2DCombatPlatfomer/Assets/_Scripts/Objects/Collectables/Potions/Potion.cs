using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : MonoBehaviour, ICollectable
{
    public UnityEvent OnPotionUsed;
    public UnityEvent OnPotionCannotBeUsed;

    public abstract void ApplyEffects(Collider2D collector);

    public abstract bool CanApplyEffects(Collider2D collector);

    public void Collect(Collider2D collector)
    {
        if(CanApplyEffects(collector)){
            ApplyEffects(collector);
            OnPotionUsed?.Invoke();
            DestroyPotion();
        }else{
            OnPotionCannotBeUsed?.Invoke();
        }
    }

    public void DestroyPotion(){
        Destroy(gameObject);
    }
}
