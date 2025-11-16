using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PianoKeySound : MonoBehaviour
{
    [Header("Sonido de esta tecla")]
    public AudioClip noteClip;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Llamado desde el sistema de entrada cuando se pulsa esta tecla
    public void PlayNote()
    {
        if (noteClip != null)
        {
            audioSource.clip = noteClip;
            audioSource.Play();
        }
    }
}
