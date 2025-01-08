using System;
using Spawners;

namespace StateMachine.States
{
    public class PrintGameState : AbstractMSM
    {
        readonly ImageSpawnManager _spawnManager;

        public PrintGameState(GameManager manager) : base(manager)
        {
            _spawnManager = manager.modelSpawnManager;
            manager.hintPanel.Show("To log in, touch your library card to the NFC field (click the field)" );
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
            _spawnManager.Update3DModels();
        }

        public override void Exit()
        {
            base.Exit();
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Printer] = false;
            _spawnManager.Update3DModels();
            _spawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return typeof(PrintGameState);
        }
    }
}
