using System.Collections.Generic;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class CloneBallPickUp : PickUp
    {
        #region Variables

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
            List<Ball> currentBalls = LevelService.Instance.GetAllBalls();
            foreach (Ball ball in currentBalls)
            {
                for (int i = 0; i < count; i++)
                {
                    Instantiate(ball.gameObject, ball.transform.position, Quaternion.identity);
                }
            }

            #endregion
        }
    }
}