using System;
using UnityEngine;

namespace StateMachine
{
    public class StartState : AbstractMSM
    {

        public StartState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enter StartState");
#endif
        }

        public override void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("Execute StartState");
#endif
        }

        public override void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit StartState");
#endif
        }

        public override Type GetNextState()
        {
            if (Manager.spawnManager.canPlace)
                return typeof(EasterEggState);
            return typeof(StartState);
        }
    }
}
