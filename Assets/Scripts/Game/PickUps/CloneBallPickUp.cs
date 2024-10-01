using System.Collections.Generic;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class CloneBallPickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _cloneBall = 2;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            CloneBalls(_cloneBall);
            AudioService.Instance.PlaySfx(_explosionAudioClip);
        }

        private void CloneBalls(int count)
        {
            List<Ball> currentBalls = LevelService.Instance.GetAllBalls();
            foreach (Ball ball in currentBalls)
            {
                for (int i = 0; i < count; i++)
                {
                    Ball newBall = ball.Clone();
                    newBall.GetRandomStartDirection();
                }
            }

            #endregion
        }
    }
}