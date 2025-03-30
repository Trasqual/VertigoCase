namespace UISystem.RouletteGame.Core
{
    public class RouletteGameSpinningState : RouletteGameState
    {
        public RouletteGameSpinningState(RouletteGameController controller) : base(controller)
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