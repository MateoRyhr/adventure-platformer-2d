using UnityEngine;

public class FrontProjectileAttackOnAir : AirAttack
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private Projectile _projectil;

    public override void PerformAttack(){
        Projectile projectil = GameObject.Instantiate(_projectil.gameObject,_castPoint.position,Quaternion.identity,null).GetComponent<Projectile>();
        projectil.Impulse(Direction());
    }

    Vector2 Direction() => Vector2.right * attackingEntity.Collider.transform.lossyScale.x;
}
