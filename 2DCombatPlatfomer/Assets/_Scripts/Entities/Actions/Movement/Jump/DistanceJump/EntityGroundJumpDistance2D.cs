using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGroundJumpDistance2D : EntityJumpDistance2D
{
    public override bool CanJump() => entityStatus.IsOnGround();
}
