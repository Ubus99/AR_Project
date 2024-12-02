using System;

namespace StateMachine
{
    public class ChargeGameState : AbstractMSM
    {
        public ChargeGameState(GameManager manager) : base(manager)
        {
        }

        public override Type GetNextState()
        {
            return typeof(EasterEggState<PrintGameState>);
        }
    }
}
