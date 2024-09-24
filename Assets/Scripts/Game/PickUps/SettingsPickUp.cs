using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class SettingsPickUp : PickUp
    {
        #region Variables

        [Header("Settings Vector3")]
        [SerializeField] private Vector3 _ballScaleChange = new(0.5f, 0.5f, 0);
        [SerializeField] private Vector3 _platformScaleChange = new(0.5f, 0.5f, 0);

        [Header("Settings Score")]
        [SerializeField] private int _score = 1;

        [Header("Ball Speed Settings")]
        [SerializeField] private float _ballSpeedChange = 1.0f;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            HandleBallSize();
            HandleScore();
            HandlePlatformSize();
            HandleBallSpeed();
        }

        #endregion

        #region Private methods

        private void HandleBallSize()
        {
            Ball[] balls = { FindObjectOfType<Ball>() };
            foreach (Ball ball in balls)
            {
                ball.transform.localScale += _ballScaleChange;
            }
        }

        private void HandleBallSpeed()
        {
            Ball ball = FindObjectOfType<Ball>();
            if (ball != null)
            {
                ball.ChangeSpeed(_ballSpeedChange);
            }
        }

        private void HandlePlatformSize()
        {
            Platform platform = FindObjectOfType<Platform>();
            if (platform != null)
            {
                platform.transform.localScale += _platformScaleChange;
            }
        }

        private void HandleScore()
        {
            GameService.Instance.AddScore(_score);
        }

        #endregion
    }
}