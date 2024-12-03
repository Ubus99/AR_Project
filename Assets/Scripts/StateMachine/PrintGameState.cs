using System;
using Spawners;

namespace StateMachine
{
    public class PrintGameState : AbstractMSM
    {
        readonly ImageSpawnManager _spawnManager;

        public PrintGameState(GameManager manager) : base(manager)
        {
            _spawnManager = manager.modelSpawnManager;
        }

        public override void Enter()
        {
            base.Enter();
            _spawnManager.enabled = true;
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Printer] = true;
        }

        public override void Execute()
        {
            base.Execute();
            _spawnManager.Update3DTracking();
        }

        public override void Exit()
        {
            base.Exit();
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Printer] = false;
            _spawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return typeof(PrintGameState);
        }
    }
}
