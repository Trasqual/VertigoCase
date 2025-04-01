using UISystem.RouletteGame.Core;

namespace StateMachineSystem.RouletteGameStates
{
    public class RouletteGameInactiveState : RouletteGameState
    {
        public RouletteGameInactiveState(RouletteGameController controller) : base(controller)
        {
        }

        public override void EnterState()
        {
            _controller.ExitButton.gameObject.SetActive(false);
            _controller.SpinButton.gameObject.SetActive(false);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}