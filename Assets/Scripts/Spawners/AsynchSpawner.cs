using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Spawners
{
    public class AsyncSpawner : InstanceManager
    {

        AsyncInstantiateOperation<GameObject> _instantiateOperation;

        public bool inProgress { get; protected set; }

        void Update()
        {
            if (_instantiateOperation is not { isDone: true } || !inProgress)
                return;

            instance = _instantiateOperation.Result[0];
            _instantiateOperation = null;
            _OnInstantiationComplete();
            inProgress = false;
        }


        public override void Spawn(GameObject prefab, Transform parent, Vector3 point)
        {
            if (!CanInstantiate(prefab)) //prevent double init
                return;

            _instantiateOperation = InstantiateAsync(prefab, parent, point, Quaternion.identity);
            inProgress = true;
        }

        public override void Spawn(GameObject prefab, Vector3 point)
        {
            if (!CanInstantiate(prefab))
                return;

            _instantiateOperation = InstantiateAsync(prefab, point, Quaternion.identity);
            inProgress = true;
        }

        bool CanInstantiate(GameObject prefab)
        {
            return prefab != null && //do not overwrite
                instance == null && // make sure prefab exists
                //!inProgress &&
                _instantiateOperation is not { isDone: false }; //prevent double init
        }

        public void Destroy()
        {
            Destroy(instance);
            inProgress = false;
        }
    }
}
