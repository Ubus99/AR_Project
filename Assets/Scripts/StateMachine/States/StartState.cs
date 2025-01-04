using System;
using Spawners;
using UnityEngine;

namespace StateMachine.States
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
            Manager.hintPanel.Show("this is a test");
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
            Manager.eggSpawnManager.eggPtr = EggSpawnManager.MacIdx;
            return typeof(EasterEggState<UploadGameState>);
        }
    }
}
