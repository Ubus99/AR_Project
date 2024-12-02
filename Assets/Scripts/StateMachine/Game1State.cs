using System;

namespace StateMachine
{
    public class Game1State : AbstractMSM
    {
        public Game1State(GameManager manager) : base(manager)
        {
        }

        public override Type GetNextState()
        {
            return typeof(EasterEggState<UploadGameState>);
        }
    }
}
