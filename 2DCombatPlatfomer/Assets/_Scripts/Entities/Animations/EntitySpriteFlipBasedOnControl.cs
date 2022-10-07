using UnityEngine;

public class EntitySpriteFlipBasedOnControl : MonoBehaviour
{
    [SerializeField] private EntityMovementController2D controller;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(controller.Direction.x > 0) _sprite.flipX = false;
        if(controller.Direction.x < 0) _sprite.flipX = true;
    }
}
