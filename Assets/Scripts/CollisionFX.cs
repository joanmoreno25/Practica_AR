using UnityEngine;

public class CollisionFX : MonoBehaviour
{
    [Header("Configuración de la colisión")]
    [SerializeField] private string otherTag = "Marker2";   // Tag del objeto con el que debe colisionar

    [Header("Efectos")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particles;

    private bool hasTriggered = false;
    private Move moveScript;

    private void Start()
    {
        // Referencia al script Move del mismo objeto
        moveScript = GetComponent<Move>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar la primera vez y solo con el objeto correcto
        if (hasTriggered) return;
        if (!other.CompareTag(otherTag)) return;

        hasTriggered = true;

        // Sonido
        if (audioSource != null)
            audioSource.Play();

        // Partículas
        if (particles != null)
            particles.Play();

        // Avisar al script de movimiento para que empiece a volver
        if (moveScript != null)
            moveScript.StartReturn();
    }
}
