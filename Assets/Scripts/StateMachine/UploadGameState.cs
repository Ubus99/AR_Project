using System;

namespace StateMachine
{
    public class UploadGameState : AbstractMSM
    {
        public UploadGameState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override Type GetNextState()
        {
            return typeof(EasterEggState<ChargeGameState>);
        }
    }
}
