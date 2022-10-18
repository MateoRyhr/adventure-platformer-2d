using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAirJumpDistance2D : EntityJumpDistance2D
{
    private bool _jumpIsActive = false;

    private void Awake()
    {
        OnJumpStart.AddListener(() => _jumpIsActive = false);    
    }

    private void Update()
    {
        if(entityStatus.IsOnGround()) _jumpIsActive = true;    
    }

    public override bool CanJump() => !entityStatus.IsOnGround() && _jumpIsActive;
}
