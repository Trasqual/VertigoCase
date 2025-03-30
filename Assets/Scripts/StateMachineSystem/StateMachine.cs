using System;
using UnityEngine;

namespace StateMachineSystem
{
    public class StateMachine
    {
        public event Action<IState> OnStateChanged;

        public IState CurrentState { get; private set; }

        public void ChangeState(IState state)
        {
            if (CurrentState != null && CurrentState.GetType() == state.GetType())
            {
                return;
            }

            CurrentState?.ExitState();
            CurrentState = state;
            CurrentState.EnterState();

            OnStateChanged?.Invoke(CurrentState);
        }

        public void UpdateStates()
        {
            CurrentState?.UpdateState();
        }
    }
}
