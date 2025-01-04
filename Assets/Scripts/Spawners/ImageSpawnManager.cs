using System;
using System.Collections.Generic;
using UI;
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
        TrackableId _chargerReference;
        GameObject _printerInstance;
        TrackableId _printerReference;

        public PrinterUIController printerUI { get; private set; }
        public ChargerUIController chargerUI { get; private set; }

        public Dictionary<PossibleObjects, bool> visibleObjects { get; private set; }

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
                        _printerReference = img.trackableId;
                        printerUI = _printerInstance.GetComponentInChildren<PrinterUIController>();
                        break;

                    case "ar_marker_2":
                        if (_chargerInstance)
                            return;
                        _chargerInstance = TryInstantiateModel(
                            img,
                            chargerPrefab,
                            visibleObjects[PossibleObjects.Charger]);
                        _chargerReference = img.trackableId;
                        chargerUI = _chargerInstance.GetComponentInChildren<ChargerUIController>();
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

        public void Update3DModels()
        {
            foreach (var img in _arTrackedImageManager.trackables)
            {
                if (img.trackableId == _chargerReference)
                {
                    Update3DModel(img,
                        visibleObjects[PossibleObjects.Charger],
                        _chargerInstance);
                }
                else if (img.trackableId == _printerReference)
                {
                    Update3DModel(img,
                        visibleObjects[PossibleObjects.Printer],
                        _printerInstance);
                }
            }
        }

        static void Update3DModel(ARTrackedImage img, bool visibility, GameObject inst)
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
            if (Quaternion.Angle(instTransf.rotation, imgTransf.rotation) > 10)
            {
                var lookDirection = imgTransf.forward;
                instTransf.rotation.SetLookRotation(lookDirection);
            }

#if UNITY_EDITOR
/*
Debug.DrawRay(imgTransf.position, imgTransf.right / 2, Color.red);
Debug.DrawRay(imgTransf.position, imgTransf.up / 2, Color.green);
Debug.DrawRay(imgTransf.position, imgTransf.forward / 2, Color.blue);
*/
#endif

        }
    }
}
