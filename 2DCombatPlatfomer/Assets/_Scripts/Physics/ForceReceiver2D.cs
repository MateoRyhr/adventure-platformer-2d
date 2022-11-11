using UnityEngine;

public class ForceReceiver2D : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier;
    public float ForceMultiplier { get => _forceMultiplier; }
}
