using UnityEngine;

public class S_PlayAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audio;

    private void OnEnable()
    {
        audio.enabled = true;
        audio.Play();
    }
}
