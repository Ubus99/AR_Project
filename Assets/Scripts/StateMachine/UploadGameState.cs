﻿using System;

namespace StateMachine
{
    public class UploadGameState : AbstractMSM
    {
        public UploadGameState(GameManager manager) : base(manager)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Manager.spawnManager.enabled = true;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
            Manager.spawnManager.enabled = false;
        }

        public override Type GetNextState()
        {
            return typeof(UploadGameState);
        }
    }
}
