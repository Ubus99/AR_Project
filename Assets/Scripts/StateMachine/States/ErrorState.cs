using System;
using StateMachine.States;
using UnityEngine;

namespace StateMachine
{
    public class ErrorState : AbstractMSM
    {

        public ErrorState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter ErrorState");
        }

        public override void Execute()
        {
            Debug.Log("Execute ErrorState");
        }

        public override void Exit()
        {
            Debug.Log("Exit ErrorState");
        }

        public override Type GetNextState()
        {
            return typeof(ErrorState);
        }
    }
}
