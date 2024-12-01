using System;
using UnityEngine;

namespace StateMachine
{
    public class EasterEggState : AbstractMSM
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
                !Manager.eggSpawnManager.eggInstance)
                Manager.eggSpawnManager.SpawnEgg();

            if (!Manager.tapStartThisFrame)
                return;

            var ray = Camera.main.ScreenPointToRay(Manager.mTapStartPosition);
            if (!Physics.Raycast(ray, out var hit))
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
            if (_eggFound)
            {
                return typeof(PrintGameState);
            }
            return typeof(EasterEggState);
        }
    }
}
