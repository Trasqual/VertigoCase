using UISystem.RouletteGame.Core;

namespace StateMachineSystem.RouletteGameStates
{
    public abstract class RouletteGameState : IState
    {
        protected RouletteGameController _controller;

        public RouletteGameState(RouletteGameController controller)
        {
            _controller = controller;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();
    }
}