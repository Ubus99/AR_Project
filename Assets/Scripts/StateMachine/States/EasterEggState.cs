using System;
using System.Linq;
using Spawners;
using UnityEngine;

namespace StateMachine.States
{
    public class EasterEggState<T> : AbstractMSM where T : AbstractMSM
    {

        bool _eggFound;

        public EasterEggState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Manager.eggSpawnManager.enabled = true;
        }

        public override void Execute()
        {
            base.Execute();
            if (Manager.eggSpawnManager.canPlace &&
                !_eggFound &&
                !Manager.eggSpawnManager.instance)
                Manager.eggSpawnManager.SpawnEgg(3);

            if (!Manager.tapStartThisFrame)
                return;

            var ray = Camera.main.ScreenPointToRay(Manager.mTapStartPosition);
            var hits = Physics.RaycastAll(ray);
            if (hits.Length <= 0)
                return;
            
            if (!hits.Any(hit =>
                {
                    bool all = false;
                    if (hit.transform.parent)
                        all |= hit.transform.parent.CompareTag(EggSpawnManager.EggTag);
                    all |= hit.transform.CompareTag(EggSpawnManager.EggTag);
                    return all;
                }))
                return;

            Manager.eggSpawnManager.DestroyEgg();
            _eggFound = true;

        }

        public override void Exit()
        {
            base.Exit();

            Manager.eggSpawnManager.DestroyEgg();
            _eggFound = true;

            Manager.eggSpawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return _eggFound ? typeof(T) : typeof(EasterEggState<T>);
        }
    }
}
