using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class ARSpawnManager : MonoBehaviour
{
    [FormerlySerializedAs("prefab")]
    public GameObject printerPrefab;

    public GameObject eggPrefab;

    [FormerlySerializedAs("_eggInstance")]
    public GameObject eggInstance;

    readonly Dictionary<TrackableId, GameObject> _instances = new Dictionary<TrackableId, GameObject>();

    ARTrackedImageManager _arTrackedImageManager;
    ARPlaneManager _planeManager;

    List<ARPlane> _planes = new List<ARPlane>();

    float surfaceArea
    {
        get => _planes.Sum(plane => plane.size.sqrMagnitude);
    }

    public bool canPlace
    {
        get =>
            _planes.Count > 0 && // is not empty
            surfaceArea > 50; // is big enough
    }

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        _planeManager = FindObjectOfType<ARPlaneManager>();
    }

    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChange;
        _planeManager.planesChanged += OnUpdatePlanes;
    }

    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChange;
        _planeManager.planesChanged -= OnUpdatePlanes;
    }

    void OnUpdatePlanes(ARPlanesChangedEventArgs arPlanesChangedEventArgs)
    {
        var buffer = new List<ARPlane>();
        foreach (var plane in _planeManager.trackables)
        {
            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                buffer.Add(plane);
            }
        }
        _planes = buffer;
    }

    void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
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

    public void SpawnEgg()
    {
        if (eggPrefab == null || eggInstance != null || _planeManager.trackables.count == 0)
            return;

        var targetPlane = _planes[Random.Range(0, _planes.Count - 1)];

        var offset = targetPlane.center - (Vector3)targetPlane.extents;
        var point = offset + Vector3.Scale(
            targetPlane.extents,
            new Vector3(Random.value, Random.value, Random.value)
        );

        eggInstance = Instantiate(eggPrefab, point, Quaternion.identity);
    }

    public void DestroyEgg()
    {
        Destroy(eggInstance);
    }

    public void Update3DTracking()
    {
        foreach (var img in _arTrackedImageManager.trackables)
        {
            var instTransf = _instances[img.trackableId].transform;
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
