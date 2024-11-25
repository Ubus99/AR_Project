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
            Debug.Log("Entering " + GetType());
#endif
        }

        public virtual void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Executing " + GetType());
#endif
        }

        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exiting " + GetType());
#endif
        }

        public abstract Type GetNextState();
    }
}
