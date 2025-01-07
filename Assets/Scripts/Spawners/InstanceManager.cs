using UnityEngine;

namespace Spawners
{
    public abstract class InstanceManager : MonoBehaviour
    {
        public delegate void InstantiationComplete();

        public GameObject instance { get; protected set; }

        public event InstantiationComplete OnInstantiationComplete;

        virtual protected void _OnInstantiationComplete()
        {
            OnInstantiationComplete?.Invoke();
        }

        public abstract void Spawn(GameObject prefab, Transform parent, Vector3 point);
        public abstract void Spawn(GameObject prefab, Vector3 point);
    }
}
