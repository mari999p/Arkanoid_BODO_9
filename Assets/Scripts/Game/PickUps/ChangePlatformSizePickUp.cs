using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangePlatformSizePickUp : PickUp
    {
        #region Variables

        [SerializeField] private float _sizeChange;
        [SerializeField] private int _points;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Protected methods

        protected override void PerformActions()
        {
            base.PerformActions();

            AudioService.Instance.PlaySfx(_explosionAudioClip);
            GameService.Instance.AddScore(_points);

            Platform platform = FindObjectOfType<Platform>();
            platform.transform.localScale *= 1 + _sizeChange / 100;
        }

        #endregion
    }
}