using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Referencia al otro objeto")]
    [SerializeField] private string otherTag = "Marker2";   // Tag del objeto al que me atraigo

    [Header("Parámetros de atracción")]
    [SerializeField] private float attractionDistance = 0.15f; // Distancia a la que empieza la atracción
    [SerializeField] private float stopDistance = 0.01f;       // Distancia a la que consideramos que ya ha llegado
    [SerializeField] private float speed = 0.6f;               // 👈 Velocidad (antes 0.2)

    private Transform other;

    void Update()
    {
        // Buscar el otro objeto (Marker2) por tag la primera vez
        if (other == null)
        {
            GameObject otherObj = GameObject.FindGameObjectWithTag(otherTag);
            if (otherObj == null)
                return; // Aún no existe en escena
            other = otherObj.transform;
        }

        float distance = Vector3.Distance(transform.position, other.position);

        // Si estamos demasiado lejos, no hacemos nada
        if (distance > attractionDistance)
            return;

        // Si ya estamos casi pegados, lo clavamos y paramos
        if (distance <= stopDistance)
        {
            transform.position = other.position;
            return;
        }

        // Movimiento suave hacia el otro objeto
        transform.position = Vector3.MoveTowards(
            transform.position,
            other.position,
            speed * Time.deltaTime
        );
    }
}
