using System;
using Unity.VisualScripting;

namespace StateMachine
{
    public interface IState
    {
        public  void Enter();
        public  void Execute();
        public  void Exit();
        public Type GetNextState();
    }
}
