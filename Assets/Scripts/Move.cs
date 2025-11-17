using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Referencia al otro objeto")]
    [SerializeField] private string otherTag = "Marker2";

    [Header("Parámetros de atracción")]
    [SerializeField] private float attractionDistance = 0.15f;
    [SerializeField] private float speed = 0.6f;

    [Header("Parámetros de retorno")]
    [SerializeField] private float returnSpeed = 0.2f;  

    [Header("Parámetros de contacto")]
    [SerializeField] private float contactDistance = 0.05f;

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
        // Si ya ha hecho ida y vuelta, no se hace nada más
        if (finished)
            return;

        // Buscar el otro objeto la primera vez
        if (other == null)
        {
            GameObject otherObj = GameObject.FindGameObjectWithTag(otherTag);
            if (otherObj == null)
                return;
            other = otherObj.transform;
        }

        // Fase de retorno
        if (returning)
        {
            // Vuelve lentamente a la posición original local
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                originalLocalPosition,
                returnSpeed * Time.deltaTime
            );

            // Cuando llegue, se fija la posición y se marca como terminado
            if (Vector3.Distance(transform.localPosition, originalLocalPosition) < 0.001f)
            {
                transform.localPosition = originalLocalPosition;
                finished = true;
            }

            return;
        }

        // Fase de atracción
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

        // Colocar el cubo justo tocando el otro, sin que se metan uno dentro del otro
        Vector3 dir = (transform.position - other.position).normalized;
        
        if (dir.sqrMagnitude < 0.0001f)
            dir = Vector3.forward;

        transform.position = other.position + dir * contactDistance;

        // Activar fase de retorno
        returning = true;
    }
}
