using System;
using UnityEngine;

namespace StateMachine
{
    public class StartState : IState
    {

        public void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enter StartState");
#endif
        }

        public void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Execute StartState");
#endif
        }

        public void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit StartState");
#endif
        }

        public Type GetNextState()
        {
            return typeof(EasterEggState);
        }
    }
}
