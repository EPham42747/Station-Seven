using UnityEngine;

public class PlaySound : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip build;
    
    public void PlayClick() {
        Debug.Log("played");
        audioSource.clip = click;
        audioSource.Play();
    }

    public void PlayBuild() {
        audioSource.clip = build;
        audioSource.Play();
    }
}
