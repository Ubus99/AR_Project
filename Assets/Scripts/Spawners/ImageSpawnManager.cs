using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class ImageSpawnManager : MonoBehaviour
    {

        public enum PossibleObjects
        {
            Charger,
            Printer,
        }

        public GameObject chargerPrefab;
        public GameObject printerPrefab;

        ARTrackedImageManager _arTrackedImageManager;

        GameObject _chargerInstance;
        GameObject _printerInstance;

        public Dictionary<PossibleObjects, bool> visibleObjects { get; set; }

        void Awake()
        {
            _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }

        void OnEnable()
        {
            visibleObjects = new Dictionary<PossibleObjects, bool>
            {
                { PossibleObjects.Charger, false },
                { PossibleObjects.Printer, false },
            };

            _arTrackedImageManager.trackedImagesChanged += OnImageChange;
        }

        void OnDisable()
        {
            _arTrackedImageManager.trackedImagesChanged -= OnImageChange;
        }

        void OnImageChange(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var img in args.updated)
            {
                switch (img.referenceImage.name)
                {
                    case "ar_marker_1":
                        if (_printerInstance)
                            return;
                        _printerInstance = TryInstantiateModel(
                            img,
                            printerPrefab,
                            visibleObjects[PossibleObjects.Printer]);
                        break;

                    case "ar_marker_2":
                        if (_chargerInstance)
                            return;
                        _chargerInstance = TryInstantiateModel(
                            img,
                            printerPrefab,
                            visibleObjects[PossibleObjects.Charger]);
                        break;

                    default:
                        Debug.Log("unknown reference image");
                        return; // careful, returns here!
                }
            }
        }

        static GameObject TryInstantiateModel(ARTrackedImage img, GameObject prefab, bool visibility)
        {

            var go = Instantiate(
                prefab,
                img.transform.parent,
                false);
            go.SetActive(visibility);

#if UNITY_EDITOR
            try
            {
                go.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
#endif
            return go;
        }

        public void Update3DTracking()
        {
            foreach (var img in _arTrackedImageManager.trackables)
            {
                Update3DTracking(img,
                    visibleObjects[PossibleObjects.Charger],
                    _chargerInstance);
                Update3DTracking(img,
                    visibleObjects[PossibleObjects.Printer],
                    _printerInstance);
            }
        }

        static void Update3DTracking(ARTrackedImage img, bool visibility, GameObject inst)
        {
            if (!inst) return;

            if (!visibility) // hide and leave
            {
                inst.SetActive(false);
                return;
            }

            var instTransf = inst.transform;
            var imgTransf = img.transform;

            if (img.trackingState == TrackingState.None)
            {
                instTransf.gameObject.SetActive(false);
                return;
            }

            instTransf.gameObject.SetActive(true);
            instTransf.position = imgTransf.position;
            if (Quaternion.Angle(instTransf.rotation, imgTransf.rotation) > 2)
                instTransf.rotation.SetLookRotation(img.transform.forward);

#if UNITY_EDITOR
            Debug.DrawRay(imgTransf.position, imgTransf.right / 2, Color.red);
            Debug.DrawRay(imgTransf.position, imgTransf.up / 2, Color.green);
            Debug.DrawRay(imgTransf.position, imgTransf.forward / 2, Color.blue);
#endif

        }
    }
}
