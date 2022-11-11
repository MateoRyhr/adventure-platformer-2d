using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundAttack : Attack
{
    public override bool CanAttack() =>
        attackingEntity.EntityStatus.IsOnGround() &&
        attackStatus == AttackStatus.notStarted &&
        !attackingEntity.EntityStatus.IsAttacking;

    public override bool WasCancelled() =>
        !attackingEntity.EntityStatus.IsOnGround() ||
        attackingEntity.EntityStatus.IsBeingAffectedByAnExternalForce;
}
