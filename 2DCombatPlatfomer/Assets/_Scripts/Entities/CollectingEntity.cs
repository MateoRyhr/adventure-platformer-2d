using UnityEngine;

public class CollectingEntity : MonoBehaviour
{
    private Collider2D _collider;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ICollectable collectable = other.gameObject.GetComponent<ICollectable>();
        if(collectable != null)
            collectable.Collect(_collider);
    }
}
