using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class ImageSpawnManager : MonoBehaviour
    {
        [FormerlySerializedAs("prefab")]
        public GameObject printerPrefab;

        readonly Dictionary<TrackableId, GameObject> _instances = new Dictionary<TrackableId, GameObject>();

        ARTrackedImageManager _arTrackedImageManager;

        void Awake()
        {
            _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }

        void OnEnable()
        {
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
                if (!_instances.ContainsKey(img.trackableId))
                {
                    var go = Instantiate(printerPrefab, img.transform.parent, false);
                    try
                    {
                        go.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    _instances.Add(img.trackableId, go);
                }

                Debug.Log(img.name);
            }
        }

        public void Update3DTracking()
        {
            foreach (var img in _arTrackedImageManager.trackables)
            {
                if (!_instances.TryGetValue(img.trackableId, out var inst))
                    continue;
                var instTransf = inst.transform;
                var imgTransf = img.transform;

                if (img.trackingState == TrackingState.None)
                {
                    instTransf.gameObject.SetActive(false);
                    continue;
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
}
