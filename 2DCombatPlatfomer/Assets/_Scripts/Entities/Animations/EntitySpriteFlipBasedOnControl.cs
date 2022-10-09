using UnityEngine;

public class EntitySpriteFlipBasedOnControl : MonoBehaviour
{
    [SerializeField] private EntityMovementController2D controller;

    private SpriteRenderer _sprite;
    private bool _flipped;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(controller.Direction.x > 0) _flipped = false;
        if(controller.Direction.x < 0) _flipped = true;
    }

    void LateUpdate()
    {
        if(!_flipped){  
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x),transform.localPosition.y,transform.localPosition.z);
        }
        if(_flipped){
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1,transform.localScale.y,transform.localScale.z);
            transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x) * -1,transform.localPosition.y,transform.localPosition.z);
        }
    }
}
