using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirAttack : Attack
{
    public override bool CanAttack() =>
        attackStatus == AttackStatus.notStarted &&
        !attackingEntity.EntityStatus.IsAttacking;

    public override bool WasCancelled() =>
        attackingEntity.EntityStatus.IsBeingAffectedByAnExternalForce;
}
