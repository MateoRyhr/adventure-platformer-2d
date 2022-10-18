using UnityEngine;

public class EntitySpriteFlipBasedOnControl : MonoBehaviour
{
    [Tooltip("The object that contains all the character")]
    [SerializeField] private Transform entityParent;
    [SerializeField] private EntityMovementController2D controller;

    private bool _flipped;

    private void Update()
    {
        if(controller.Direction.x > 0) _flipped = false;
        if(controller.Direction.x < 0) _flipped = true;
        UpdateTransform();
    }
    
    void UpdateTransform()
    {
        if(!_flipped)
            entityParent.localScale = new Vector3(Mathf.Abs(entityParent.localScale.x),entityParent.localScale.y,entityParent.localScale.z);
        else
            entityParent.localScale = new Vector3(Mathf.Abs(entityParent.localScale.x) * -1,entityParent.localScale.y,entityParent.localScale.z);
    }
}
