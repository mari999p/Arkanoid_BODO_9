using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ExplosivePickUp : PickUp
    {
        #region Variables

        [Header(nameof(ExplosivePickUp))]
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private Sprite _ballSprite;
        [SerializeField] private Gradient _ballCGradient;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            foreach (Ball ball in LevelService.Instance.Balls)
            {
                ball.MakeExplosive(_explosionRadius, _ballSprite, _ballCGradient);
            }
        }

        #endregion
    }
}