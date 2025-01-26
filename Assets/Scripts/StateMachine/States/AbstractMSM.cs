using System;
using UnityEngine;

namespace StateMachine.States
{
    public abstract class AbstractMSM : IState
    {

        readonly protected GameManager Manager;
        protected bool InProcess;

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
            if (!InProcess)
                Debug.Log("Executing " + GetType());
#endif
            InProcess = true;
        }

        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exiting " + GetType());
#endif
            InProcess = false;
        }

        public abstract Type GetNextState();
    }
}
