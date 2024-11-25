using System;
using UnityEngine;

namespace StateMachine
{
    public abstract class AbstractMSM : IState
    {

        readonly protected GameManager Manager;

        protected AbstractMSM(GameManager manager)
        {
            Manager = manager;
        }

        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enter " + GetType());
#endif
        }

        public virtual void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Enter " + GetType());
#endif
        }

        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Enter " + GetType());
#endif
        }

        public abstract Type GetNextState();
    }
}
