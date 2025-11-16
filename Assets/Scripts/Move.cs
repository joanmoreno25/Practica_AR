using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Referencia al otro objeto")]
    [SerializeField] private string otherTag = "Marker2";

    [Header("Parámetros de atracción")]
    [SerializeField] private float attractionDistance = 0.15f;
    [SerializeField] private float speed = 0.6f;

    [Header("Parámetros de retorno")]
    [SerializeField] private float returnSpeed = 0.2f;   // 👈 RETORNO LENTO Y SUAVE

    private Transform other;
    private Vector3 originalLocalPosition;
    private bool returning = false;
    private bool finished = false;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (finished)
            return;

        if (other == null)
        {
            GameObject otherObj = GameObject.FindGameObjectWithTag(otherTag);
            if (otherObj == null)
                return;
            other = otherObj.transform;
        }

        // --- FASE DE RETORNO (después de la colisión) ---
        if (returning)
        {
            // Movimiento más lento, fluido y controlado
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                originalLocalPosition,
                returnSpeed * Time.deltaTime
            );

            // Cuando llega, terminamos el ciclo
            if (Vector3.Distance(transform.localPosition, originalLocalPosition) < 0.001f)
            {
                transform.localPosition = originalLocalPosition;
                finished = true;
            }

            return;
        }

        // --- FASE DE ATRACCIÓN ---
        float distance = Vector3.Distance(transform.position, other.position);
        if (distance > attractionDistance)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            other.position,
            speed * Time.deltaTime
        );
    }

    // Llamado desde CollisionFX al colisionar
    public void StartReturn()
    {
        if (finished)
            return;

        returning = true;
    }
}
