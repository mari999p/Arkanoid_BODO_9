using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeLifePickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _lifeChange;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            if (_lifeChange > 0)
            {
                GameService.Instance.AddLife(_lifeChange);
            }
            else
            {
                GameService.Instance.RemoveLife();
            }
        }

        #endregion
    }
}