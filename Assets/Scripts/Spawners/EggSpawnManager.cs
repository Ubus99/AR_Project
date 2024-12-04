using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EggSpawnManager : MonoBehaviour
    {
        public static string EggName = "Egg";

        public GameObject eggPrefab;
        public float requiredArea = 75;

        ARPlaneManager _planeManager;
        List<ARPlane> _planes = new List<ARPlane>();

        public GameObject eggInstance { get; private set; }

        float surfaceArea
        {
            get => _planes.Sum(plane => plane.size.sqrMagnitude);
        }

        public bool canPlace
        {
            get =>
                _planes.Count > 0 && // is not empty
                surfaceArea > requiredArea; // is big enough
        }

        void Awake()
        {
            _planeManager = FindObjectOfType<ARPlaneManager>();
        }

        void OnEnable()
        {
            _planeManager.planesChanged += OnUpdatePlanes;
        }

        void OnDisable()
        {
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

        public void SpawnEgg(int delay)
        {
            Invoke(nameof(SpawnEgg), delay);
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
            eggInstance.name = EggName;
        }

        public void DestroyEgg()
        {
            Destroy(eggInstance);
        }
    }
}
