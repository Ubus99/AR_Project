using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Spawners
{
    public class AsyncSpawner : MonoBehaviour
    {

        public delegate void InstantiationComplete();

        AsyncInstantiateOperation<GameObject> _instantiateOperation;
        bool _waitForInstantiation;

        public GameObject instance { get; private set; }

        void Update()
        {
            if (!_waitForInstantiation || !_instantiateOperation.isDone)
                return;

            instance = _instantiateOperation.Result[0];
            _OnInstantiationComplete();
            _waitForInstantiation = false;
        }

        virtual protected void _OnInstantiationComplete()
        {
            OnInstantiationComplete?.Invoke();
        }

        public event InstantiationComplete OnInstantiationComplete;

        public void Spawn(GameObject prefab, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null || // make sure prefab exists
                _waitForInstantiation) //prevent double init
                return;

            _instantiateOperation = InstantiateAsync(prefab, point, Quaternion.identity);
            _waitForInstantiation = true;
        }

        public void Destroy()
        {
            Destroy(instance);
        }
    }
}
