using UnityEngine;

public class CollisionFX : MonoBehaviour
{
    [Header("Configuración de la colisión")]
    [SerializeField] private string otherTag = "Marker2";   // Tag del objeto con el que debe colisionar

    [Header("Efectos")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particles;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar la primera vez y solo con el objeto correcto
        if (hasTriggered) return;
        if (!other.CompareTag(otherTag)) return;

        hasTriggered = true;

        if (audioSource != null)
            audioSource.Play();

        if (particles != null)
            particles.Play();
    }
}
