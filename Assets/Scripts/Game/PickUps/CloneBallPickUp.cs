using System.Collections.Generic;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class CloneBallPickUp : PickUp
    {
        #region Variables

        [Header(nameof(CloneBallPickUp))]
        [SerializeField] private int _cloneBall = 2;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            CloneBalls(_cloneBall);
        }

        private void CloneBalls(int count)
        {
            List<Ball> currentBalls = LevelService.Instance.Balls;
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