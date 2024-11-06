using System;
using Unity.VisualScripting;

namespace StateMachine
{
    public abstract class AbstractState<TT>
    {
        static protected TT Holder;

        bool _isRunning;

        public void Update()
        {
            if (!_isRunning)
            {
                EntryBehavior();
                _isRunning = true;
            }

            UpdateBehavior();
        }

        protected void Exit()
        {
            ExitBehavior();
            _isRunning = false;
        }

        abstract protected void EntryBehavior();

        abstract protected void UpdateBehavior();

        abstract protected void ExitBehavior();
    }
}
