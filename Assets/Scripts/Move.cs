using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Referencia al otro objeto")]
    [SerializeField] private string otherTag = "Marker2";

    [Header("Parámetros de atracción")]
    [SerializeField] private float attractionDistance = 0.15f;
    [SerializeField] private float speed = 0.6f;

    [Header("Parámetros de retorno")]
    [SerializeField] private float returnSpeed = 0.2f;   // retorno lento

    [Header("Parámetros de contacto")]
    [SerializeField] private float contactDistance = 0.05f; // distancia entre centros cuando los cubos "se tocan"

    private Transform other;
    private Vector3 originalLocalPosition;
    private bool returning = false;
    private bool finished = false;

    void Start()
    {
        // Posición local original encima del marcador
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Si ya ha hecho ida + vuelta, no hacemos nada más
        if (finished)
            return;

        // Buscar el otro objeto (Marker2) la primera vez
        if (other == null)
        {
            GameObject otherObj = GameObject.FindGameObjectWithTag(otherTag);
            if (otherObj == null)
                return;
            other = otherObj.transform;
        }

        // -------- FASE DE RETORNO --------
        if (returning)
        {
            // Vuelve lentamente a la posición original local
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                originalLocalPosition,
                returnSpeed * Time.deltaTime
            );

            // Cuando llegue, fijamos la posición y marcamos como terminado
            if (Vector3.Distance(transform.localPosition, originalLocalPosition) < 0.001f)
            {
                transform.localPosition = originalLocalPosition;
                finished = true;
            }

            return;
        }

        // -------- FASE DE ATRACCIÓN --------
        float distance = Vector3.Distance(transform.position, other.position);

        // Si están lejos, no hacer nada
        if (distance > attractionDistance)
            return;

        // Movimiento suave hacia el otro objeto
        transform.position = Vector3.MoveTowards(
            transform.position,
            other.position,
            speed * Time.deltaTime
        );
    }

    // Llamado desde CollisionFX al detectar la colisión
    public void StartReturn()
    {
        if (finished || other == null)
            return;

        // 1) Colocar el cubo justo tocando el otro, sin que se metan uno dentro del otro
        Vector3 dir = (transform.position - other.position).normalized;
        // si por alguna razón están en el mismo punto, evitamos un vector (0,0,0)
        if (dir.sqrMagnitude < 0.0001f)
            dir = Vector3.forward;

        transform.position = other.position + dir * contactDistance;

        // 2) Activar fase de retorno
        returning = true;
    }
}
