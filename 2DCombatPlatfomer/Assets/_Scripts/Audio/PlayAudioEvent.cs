using UnityEngine;

public class PlayAudioEvent : MonoBehaviour
{
    [SerializeField] private AudioEvent _audioEvent;
    [SerializeField] private AudioSource _audioSource;

    public void PlayAudio(){
        _audioEvent.Play(_audioSource);
    }
}
