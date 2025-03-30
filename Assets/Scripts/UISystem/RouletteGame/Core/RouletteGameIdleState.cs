using UISystem.RouletteGame.Data;

namespace UISystem.RouletteGame.Core
{
    public class RouletteGameIdleState : RouletteGameState
    {
        public RouletteGameIdleState(RouletteGameController controller) : base(controller)
        {
        }

        public override void EnterState()
        {
            if (_controller.CurrentZoneData.ZoneType is ZoneType.Safe or ZoneType.Super)
            {
                _controller.ExitButton.gameObject.SetActive(true);
            }

            _controller.SpinButton.gameObject.SetActive(true);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
    }
}