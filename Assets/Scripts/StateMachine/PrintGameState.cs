using System;

namespace StateMachine
{
    public class PrintGameState : AbstractMSM
    {
        public PrintGameState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Manager.modelSpawnManager.enabled = true;
        }

        public override void Execute()
        {
            base.Execute();
            Manager.modelSpawnManager.Update3DTracking();
        }

        public override void Exit()
        {
            base.Exit();
            Manager.modelSpawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return typeof(PrintGameState);
        }
    }
}
