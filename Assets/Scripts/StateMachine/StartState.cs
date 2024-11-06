using System;
using UnityEngine;

namespace StateMachine
{
    public class StartState : IState
    {

        public void Enter()
        {
            Debug.Log("Enter StartState");
        }

        public void Execute()
        {
            Debug.Log("Execute StartState");
        }

        public void Exit()
        {
            Debug.Log("Exit StartState");
        }

        public Type GetNextState()
        {
            return typeof(EasterEggState);
        }
    }
}
