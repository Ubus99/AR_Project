using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Spawners
{
    public class AsyncSpawner : InstanceManager
    {

        bool _inProgress;

        AsyncInstantiateOperation<GameObject> _instantiateOperation;

        void Update()
        {
            if (_instantiateOperation is not { isDone: true } || !_inProgress)
                return;

            instance = _instantiateOperation.Result[0];
            _OnInstantiationComplete();
            _inProgress = false;
        }


        public override void Spawn(GameObject prefab, Transform parent, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null || // make sure prefab exists
                _instantiateOperation is { isDone: false }) //prevent double init
                return;

            _instantiateOperation = InstantiateAsync(prefab, parent, point, Quaternion.identity);
            _inProgress = true;
        }

        public override void Spawn(GameObject prefab, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null || // make sure prefab exists
                _instantiateOperation is { isDone: false }) //prevent double init
                return;

            _instantiateOperation = InstantiateAsync(prefab, point, Quaternion.identity);
            _inProgress = true;
        }

        public void Destroy()
        {
            Destroy(instance);
            _inProgress = false;
        }
    }
}
