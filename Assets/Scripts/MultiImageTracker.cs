using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;   // 👈 CORRECTO

namespace MyARProject
{
    public class MultiImageTracker : MonoBehaviour
    {
        public ARTrackedImageManager imageManager;

        public GameObject marker1Prefab;
        public GameObject marker2Prefab;

        private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

        void OnEnable()
        {
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        void OnDisable()
        {
            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var trackedImage in args.added)
            {
                UpdateImage(trackedImage);
            }

            foreach (var trackedImage in args.updated)
            {
                UpdateImage(trackedImage);
            }

            foreach (var trackedImage in args.removed)
            {
                string imageName = trackedImage.referenceImage.name;

                if (spawnedPrefabs.TryGetValue(imageName, out var obj))
                {
                    obj.SetActive(false);
                }
            }
        }

        private void UpdateImage(ARTrackedImage trackedImage)
        {
            string imageName = trackedImage.referenceImage.name;

            GameObject prefabToUse = null;

            if (imageName == "Marker1")
            {
                prefabToUse = marker1Prefab;
            }
            else if (imageName == "Marker2")
            {
                prefabToUse = marker2Prefab;
            }

            if (prefabToUse == null)
                return;

            GameObject spawnedObject;

            // Instanciamos una vez por marker
            if (!spawnedPrefabs.TryGetValue(imageName, out spawnedObject))
            {
                spawnedObject = Instantiate(prefabToUse, trackedImage.transform);
                spawnedPrefabs[imageName] = spawnedObject;

                // Centrado en el marcador, sin tocar la escala
                spawnedObject.transform.localPosition = Vector3.zero;
                spawnedObject.transform.localRotation = Quaternion.identity;
            }

            // Visibilidad según tracking
            spawnedObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }
    }
}
