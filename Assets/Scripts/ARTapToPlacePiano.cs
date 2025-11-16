using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlacePiano : MonoBehaviour
{
    [Header("Prefab a instanciar")]
    public GameObject pianoPrefab;

    [Header("Referencias AR")]
    public ARRaycastManager raycastManager;

    private GameObject spawnedPiano;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // --- MODO EDITOR (XR Simulation) → clic de ratón ---
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = Input.mousePosition;
            TryPlace(screenPosition);
        }
#else
        // --- MODO DISPOSITIVO REAL → touch ---
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        TryPlace(touch.position);
#endif
    }

    private void TryPlace(Vector2 screenPosition)
    {
        if (raycastManager == null)
            return;

        // Raycast contra los planos detectados
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedPiano == null)
            {
                spawnedPiano = Instantiate(pianoPrefab, hitPose.position, hitPose.rotation);
            }
            // Si quisieras moverlo después, aquí actualizarías su posición,
            // pero el ejercicio pide que solo se cree uno.
        }
    }
}
