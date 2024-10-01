using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeScorePickUp : PickUp
    {
        #region Variables

        [SerializeField] private int _scoreChange = 1;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();
            AudioService.Instance.PlaySfx(_explosionAudioClip);
            GameService.Instance.AddScore(_scoreChange);
        }

        #endregion
    }
}