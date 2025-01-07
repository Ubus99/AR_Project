using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;

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

        InstanceManager _chargerManager;
        TrackableId _chargerReference;

        InstanceManager _printerManager;
        TrackableId _printerReference;

        public PrinterUIController printerUI { get; private set; }
        public ChargerUIController chargerUI { get; private set; }

        public Dictionary<PossibleObjects, bool> visibleObjects { get; private set; }

        void Awake()
        {
            _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
            _chargerManager = gameObject.AddComponent<SyncSpawner>();
            _printerManager = gameObject.AddComponent<SyncSpawner>();
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
                        if (_printerManager.instance)
                            return;
                        _printerManager.Spawn(printerPrefab, img.transform.position);
                        _printerManager.OnInstantiationComplete += () =>
                        {
                            _printerReference = img.trackableId;
                            printerUI = _printerManager.instance.GetComponentInChildren<PrinterUIController>();
                        };
                        break;

                    case "ar_marker_2":
                        if (_chargerManager.instance)
                            return;
                        _chargerManager.Spawn(chargerPrefab, img.transform.position);
                        _printerManager.OnInstantiationComplete += () =>
                        {
                            _chargerReference = img.trackableId;
                            chargerUI = _chargerManager.instance.GetComponentInChildren<ChargerUIController>();
                        };
                        break;

                    default:
                        Debug.Log("unknown reference image");
                        return; // careful, returns here!
                }
            }
        }

        public void Update3DModels()
        {
            foreach (var img in _arTrackedImageManager.trackables)
            {
                if (img.trackableId == _chargerReference)
                {
                    if (!_chargerManager.instance)
                        return;

                    Update3DModel(img,
                        visibleObjects[PossibleObjects.Charger],
                        _chargerManager.instance);
                }
                else if (img.trackableId == _printerReference)
                {
                    if (!_printerManager.instance)
                        return;

                    Update3DModel(img,
                        visibleObjects[PossibleObjects.Printer],
                        _printerManager.instance);
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
