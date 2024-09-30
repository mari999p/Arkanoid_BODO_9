using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeBallSizePickUp : PickUp
    {
        #region Variables

        [SerializeField] private float _sizeChange;
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
                ball.transform.localScale *= 1 + _sizeChange / 100;
            }
        }

        #endregion
    }
}