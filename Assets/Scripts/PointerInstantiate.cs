using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PointerInstantiate : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject prefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Camera camera;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(touch.position);
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
            {
                //Logica para ver si ya esta instanciado o se debe instanciar
                GameObject.Instantiate(prefab, hits[0].pose.position, Quaternion.identity);
            }

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {
                //Logica para detectar distintas teclas y emitir distintos sonidos segun la tecla pulsada
                GameObject test = hit.transform.gameObject;
            }
        }
    }
}
