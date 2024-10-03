using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PickUp : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosionAudioClip;
        [SerializeField] private int _points;
        #region Unity lifecycle

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tag.Platform))
            {
                PerformActions();
                Destroy(gameObject);
            }
        }

        #endregion

        #region Protected methods

        protected virtual void PerformActions()
        {
            GameService.Instance.AddScore(_points);
            AudioService.Instance.PlaySfx(_explosionAudioClip);
                
            // TODO: Play vfx
            // TODO: Play sound
        }

        #endregion
    }
}