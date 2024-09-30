using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeScorePickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _scoreChange = 1;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            GameService.Instance.AddScore(_scoreChange);
        }

        #endregion
    }
}