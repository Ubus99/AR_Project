using System;

namespace StateMachine
{
    public abstract class AbstractMSM : IState
    {

        readonly protected GameManager Manager;
        
        protected AbstractMSM(GameManager manager)
        {
            Manager = manager;
        }

        public abstract void Enter();

        public abstract void Execute();

        public abstract void Exit();

        public abstract Type GetNextState();
    }
}
