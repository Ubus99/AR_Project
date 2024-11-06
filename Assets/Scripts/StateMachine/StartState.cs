using System;
using UnityEngine;

namespace StateMachine
{
    public class StartState : AbstractState<GameManager>
    {

        protected override void EntryBehavior()
        {
            Debug.Log("Start");
        }

        protected override void UpdateBehavior()
        {
            Debug.Log("Running");
        }

        protected override void ExitBehavior()
        {
            throw new NotImplementedException();
        }
    }
}
