using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Spawners
{
    public abstract class SyncSpawner : InstanceManager
    {

        public override void Spawn(GameObject prefab, Transform parent, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null) //make sure exists
                return;

            instance = Instantiate(prefab, point, Quaternion.identity, parent);
            _OnInstantiationComplete();
        }

        public override void Spawn(GameObject prefab, Vector3 point)
        {
            if (prefab == null || //do not overwrite
                instance != null) //make sure exists
                return;

            instance = Instantiate(prefab, point, Quaternion.identity);
            _OnInstantiationComplete();
        }

        public void Destroy()
        {
            Destroy(instance);
        }
    }
}
