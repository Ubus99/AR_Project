using System;
using UnityEngine;

namespace StateMachine
{
    public class EasterEggState : IState
    {

        public void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enter EasterEggState");
#endif
        }

        public void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Execute EasterEggState");
#endif
        }

        public void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit EasterEggState");
#endif
        }

        public Type GetNextState()
        {
            return typeof(StartState);
        }
    }
}
