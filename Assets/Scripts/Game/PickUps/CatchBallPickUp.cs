using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class CatchBallPickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _points;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            AudioService.Instance.PlaySfx(_explosionAudioClip);
            GameService.Instance.AddScore(_points);
            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ResetBall();
            }
        }

        #endregion
    }
}