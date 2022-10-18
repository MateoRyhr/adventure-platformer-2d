using UnityEngine;

public class FXInstantiator : MonoBehaviour
{
    [SerializeField] private Transform fxPosition;
    [SerializeField] private GameObject fxPrefab;
    [SerializeField] private ParticleSystem fxParticleSystem;
    [SerializeField] private float fxDuration;

    public void PlayEffect(){
        GameObject fx = Instantiate(fxPrefab,fxPosition.position,Quaternion.Euler(0,0,0),null);
        if(!fxParticleSystem.main.playOnAwake) fxParticleSystem.Play();
        Destroy(fx,fxDuration);
    }
}
