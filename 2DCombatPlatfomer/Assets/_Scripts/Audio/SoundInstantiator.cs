using UnityEngine;

public class SoundInstantiator : MonoBehaviour
{
    private float eventDelay = 0.2f;

    public void PlayAudioEvent(GameObject sound){
        GameObject soundInstance = Instantiate(sound,gameObject.transform.position,Quaternion.Euler(0,0,0),null);
        soundInstance.GetComponent<PlayAudioEvent>().PlayAudio();
        this.Invoke(() => Destroy(soundInstance),soundInstance.GetComponent<AudioSource>().clip.length + eventDelay);
    }

    public void PlayAudioSource(GameObject sound){
        GameObject soundInstance = Instantiate(sound,gameObject.transform.position,Quaternion.Euler(0,0,0),null);
        soundInstance.GetComponent<AudioSource>().Play();
        this.Invoke(() => Destroy(soundInstance),soundInstance.GetComponent<AudioSource>().clip.length + eventDelay);
    }
}
