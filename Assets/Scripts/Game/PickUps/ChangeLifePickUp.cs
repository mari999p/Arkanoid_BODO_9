using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeLifePickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _lifeChange;
        [SerializeField] private int _points;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            GameService.Instance.AddScore(_points);
            GameService.Instance.ChangeLife(_lifeChange);
        }

        #endregion
    }
}