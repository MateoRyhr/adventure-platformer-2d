using UnityEngine;

public class EntityStatusAnimation : MonoBehaviour
{
    [SerializeField] private EntityStatus2D status;

    private Animator animator;

    const string GROUND = "ground";
    const string AIR = "air";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(status.IsOnGround()) animator.SetTrigger(GROUND);
        else animator.SetTrigger(AIR);
    }
}
