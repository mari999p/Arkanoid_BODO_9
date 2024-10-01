using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ExplosivePickUp : PickUp
    {
        #region Variables

        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private int _points;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            GameService.Instance.AddScore(_points);
            AudioService.Instance.PlaySfx(_explosionAudioClip);
            if (LevelService.Instance.Ball != null)
            {
                LevelService.Instance.Ball.MakeExplosive(_explosionRadius);
            }
        }

        #endregion
    }
}