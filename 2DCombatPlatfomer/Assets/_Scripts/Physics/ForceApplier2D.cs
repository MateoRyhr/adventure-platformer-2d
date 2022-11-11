using UnityEngine;

public class ForceApplier2D : MonoBehaviour
{
    [SerializeField] public Vector2 ForceDirection;
    [SerializeField] private FloatVariable _force;
    [SerializeField] private bool _addRandomVariation;
    [SerializeField] private RangedFloat _forceOfTheVariation;

    public void ApplyForce(Rigidbody2D rigidBody,Vector2 forcePoint,Vector2 direction = default)
    {
        Vector2 forceDirection = direction != default ? direction : ForceDirection;
        rigidBody.AddForceAtPosition(ForceToApply(rigidBody,forceDirection),rigidBody.ClosestPoint(forcePoint),ForceMode2D.Impulse);
    }

    private Vector2 ForceToApply(Rigidbody2D rigidBody, Vector2 direction)
    {
        Vector2 forceToApply = direction * _force.Value;
        if(_addRandomVariation){
            Vector2 randomForce = RandomForce();
            forceToApply -= forceToApply.normalized * randomForce.magnitude;
            forceToApply += randomForce;
        }
        ForceReceiver2D forceReceiver = rigidBody.GetComponent<ForceReceiver2D>();        
        if(forceReceiver) forceToApply *= forceReceiver.ForceMultiplier;
        return forceToApply;
    }

    Vector2 RandomForce()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomForce = Random.Range(_force.Value * _forceOfTheVariation.minValue,_force.Value * _forceOfTheVariation.maxValue);
        return randomDirection * randomForce;
    }
}
