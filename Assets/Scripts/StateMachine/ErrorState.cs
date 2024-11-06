using System;
using UnityEngine;

namespace StateMachine
{
    public class ErrorState : IState
    {

        public void Enter()
        {
            Debug.Log("Enter ErrorState");
        }

        public void Execute()
        {
            Debug.Log("Execute ErrorState");
        }

        public void Exit()
        {
            Debug.Log("Exit ErrorState");
        }

        public Type GetNextState()
        {
            return typeof(ErrorState);
        }
    }
}
