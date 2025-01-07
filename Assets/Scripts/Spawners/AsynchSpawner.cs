using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Spawners
{
    public abstract class AsynchSpawner : MonoBehaviour
    {
        AsyncInstantiateOperation<GameObject> _eggInstantiateOperation;
        bool _waitForInstantiation;

        public GameObject instance { get; private set; }

        void Update()
        {
            if (!_waitForInstantiation || _eggInstantiateOperation.progress < 1)
                return;

            instance = _eggInstantiateOperation.Result[0];
            OnInstantiationComplete();
            _waitForInstantiation = false;
        }

        abstract protected void OnInstantiationComplete();

        public void SpawnEgg(int delay)
        {
            Invoke(nameof(SpawnEgg), delay);
        }

        public void Spawn(GameObject prefab, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null || // make sure prefab exists
                _waitForInstantiation) //prevent double init
                return;

            _eggInstantiateOperation = InstantiateAsync(prefab, point, Quaternion.identity);
            _waitForInstantiation = true;
        }

        public void Destroy()
        {
            Destroy(instance);
        }
    }
}
