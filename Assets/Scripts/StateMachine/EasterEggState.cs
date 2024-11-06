using System;
using UnityEngine;

namespace StateMachine
{
    public class EasterEggState : IState
    {
        public void Enter()
        {
            Debug.Log("Enter EasterEggState");
        }

        public void Execute()
        {
            Debug.Log("Execute EasterEggState");
        }

        public void Exit()
        {
            Debug.Log("Exit EasterEggState");
        }

        public Type GetNextState()
        {
            return typeof(StartState);
        }
    }
}
