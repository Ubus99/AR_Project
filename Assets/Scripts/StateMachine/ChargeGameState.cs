using System;
using Spawners;
using UI;

namespace StateMachine
{
    public class ChargeGameState : AbstractMSM
    {
        readonly PrinterUIController _printerUI;
        readonly ImageSpawnManager _spawnManager;

        public ChargeGameState(GameManager manager) : base(manager)
        {
            _spawnManager = manager.modelSpawnManager;
        }

        public override void Enter()
        {
            base.Enter();
            _spawnManager.enabled = true;
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Charger] = true;
        }

        public override void Execute()
        {
            base.Execute();
            _spawnManager.Update3DTracking();
        }

        public override void Exit()
        {
            base.Exit();
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Charger] = false;
            _spawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return typeof(EasterEggState<PrintGameState>);
        }
    }
}
