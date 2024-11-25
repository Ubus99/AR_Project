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

            Manager.spawnManager.SpawnEgg();
        }

        public override void Execute()
        {
            base.Execute();

            if (!Manager.tapStartThisFrame)
                return;

            var ray = Camera.main.ScreenPointToRay(Manager.mTapStartPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                Manager.spawnManager.DestroyEgg();
            }
        }

        public override void Exit()
        {
            base.Exit();

            Manager.spawnManager.DestroyEgg();
            _eggFound = true;
        }

        public override Type GetNextState()
        {
            if (_eggFound)
            {
                return typeof(UploadGameState);
            }
            if (Manager.spawnManager.eggInstance)
            {
                return typeof(EasterEggState);
            }
            return typeof(StartState);
        }
    }
}
