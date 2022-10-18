using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "2DCombatPlatfomer/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public float Damage;
    public float Range;
    // public AttackType AttackType;
    public float AnimationTime;
    [Tooltip("Time elapsed before attack frame")]
    public float TimeUntilAttackDone;
}
