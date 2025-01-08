using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EggSpawnManager : AsyncSpawner
    {
        public const string EggTag = "EasterEgg";
        public const int MacIdx = 0;
        public const int CardIdx = 1;
        public const int InkIdx = 2;

        public List<GameObject> eggPrefab = new List<GameObject>();
        public float requiredArea = 75;

        ARPlaneManager _planeManager;
        List<ARPlane> _planes = new List<ARPlane>();

        public int eggPtr { private get; set; } // spaghetti code... fuck it

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

        protected override void _OnInstantiationComplete()
        {
            instance.tag = EggTag;
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
            if (_planeManager.trackables.count == 0) // make sure there is ground
                return;
            Invoke(nameof(SpawnEgg), delay);
            inProgress = true;
        }

        public void SpawnEgg()
        {
            if (_planeManager.trackables.count == 0) // make sure there is ground
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

            Spawn(eggPrefab[eggPtr], point);
        }

        public void DestroyEgg()
        {
            Destroy();
        }
    }
}
