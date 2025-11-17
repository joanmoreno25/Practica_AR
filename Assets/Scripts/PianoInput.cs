using UnityEngine;

public class PianoInput : MonoBehaviour
{
    private Camera arCamera;

    void Start()
    {
        // Usar siempre la cámara principal
        arCamera = Camera.main;
    }

    void Update()
    {
        if (arCamera == null)
            return;

#if UNITY_EDITOR
        // En editor: clic de ratón
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.mousePosition;
            HandleTap(screenPos);
        }
#else
        // En dispositivo: toque
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        HandleTap(touch.position);
#endif
    }

    private void HandleTap(Vector2 screenPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Mirar si el objeto pulsado tiene un PianoKeySound
            PianoKeySound key = hit.collider.GetComponent<PianoKeySound>();
            if (key != null)
            {
                key.PlayNote();
            }
        }
    }
}
