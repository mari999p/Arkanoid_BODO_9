using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeBallSpeedPickUp : PickUp
    {
        #region Variables

        [SerializeField] private float _speedChange;
        [SerializeField] private int _points;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            GameService.Instance.AddScore(_points);
            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ChangeSpeed(_speedChange);
            }
        }

        #endregion
    }
}