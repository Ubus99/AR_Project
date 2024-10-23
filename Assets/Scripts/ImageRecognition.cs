using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class ImageRecognition : MonoBehaviour
{
    public GameObject prefab;
    readonly Dictionary<TrackableId, GameObject> _instances = new Dictionary<TrackableId, GameObject>();

    ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void Update()
    {
        foreach (var img in _arTrackedImageManager.trackables)
        {
            if (img.trackingState == TrackingState.None)
                continue;

            var instTrans = _instances[img.trackableId].transform;
            instTrans.position = img.transform.position;
            instTrans.rotation = Quaternion.Slerp(instTrans.rotation, transform.rotation, 0.5f);

            Debug.DrawRay(img.transform.position, img.transform.forward, Color.red);
        }
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
        foreach (var img in args.added)
        {
            if (!_instances.ContainsKey(img.trackableId))
            {
                var go = Instantiate(prefab, img.transform.parent, false);
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
}
