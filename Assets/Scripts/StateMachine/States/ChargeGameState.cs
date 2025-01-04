using System;
using Spawners;

namespace StateMachine.States
{
    public class ChargeGameState : AbstractMSM
    {
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
            _spawnManager.Update3DModels();
        }

        public override void Exit()
        {
            base.Exit();
            _spawnManager.visibleObjects[ImageSpawnManager.PossibleObjects.Charger] = false;
            _spawnManager.Update3DModels();
            _spawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            if (!(_spawnManager.chargerUI?.done ?? false)) // wtf does this mean?
                return typeof(ChargeGameState);

            Manager.eggSpawnManager.eggPtr = EggSpawnManager.InkIdx;
            return typeof(EasterEggState<PrintGameState>);
        }
    }
}
