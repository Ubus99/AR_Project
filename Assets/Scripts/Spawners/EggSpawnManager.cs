using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EggSpawnManager : MonoBehaviour
    {
        public const string EggTag = "EasterEgg";
        public const int MacIdx = 0;
        public const int CardIdx = 1;
        public const int InkIdx = 2;

        public List<GameObject> eggPrefab = new List<GameObject>();
        public float requiredArea = 75;

        AsyncInstantiateOperation<GameObject> _eggInstantiateOperation;

        ARPlaneManager _planeManager;
        List<ARPlane> _planes = new List<ARPlane>();

        bool _waitForInstantiation;

        public int eggPtr { private get; set; } // spaghetti code... fuck it

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

        void Update()
        {
            if (!_waitForInstantiation || _eggInstantiateOperation.progress < 1)
                return;

            eggInstance = _eggInstantiateOperation.Result[0];
            eggInstance.tag = EggTag;
            _waitForInstantiation = false;
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
            if (eggPrefab == null || //do not overwrite
                eggInstance != null || // make sure prefab exists
                _waitForInstantiation || //prevent double init
                _planeManager.trackables.count == 0) // make sure there is ground
                return;

            var targetPlane = _planes[Random.Range(0, _planes.Count - 1)];

            var diagonal = new Vector3(targetPlane.extents.x, 0, targetPlane.extents.y);
            var offset = targetPlane.center - diagonal;
            DebugDrawer.Lines["SpawnManager"] =
                new DebugDrawer.Line
                {
                    A = offset,
                    B = targetPlane.center + diagonal,
                };
            var point = offset + Vector3.Scale(
                diagonal,
                new Vector3(Random.value, Random.value, Random.value)
            );

            _eggInstantiateOperation = InstantiateAsync(eggPrefab[eggPtr], point, Quaternion.identity);
            _waitForInstantiation = true;
        }

        public void DestroyEgg()
        {
            Destroy(eggInstance);
        }
    }
}
