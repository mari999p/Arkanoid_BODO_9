using Arkanoid.Services;

namespace Arkanoid.Game.PickUps
{
    public class CatchBallPickUp : PickUp
    {
        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ResetBall();
            }
        }

        #endregion
    }
}