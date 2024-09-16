using System;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _crackedBlock;
        [SerializeField] private Sprite _heavilyCrackedBlock;
        [SerializeField] private int _hitPoints = 3;
        [SerializeField] private int _score;

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
        }

        private void OnDestroy()
        {
            GameService.Instance.AddScore(_score);
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_hitPoints > 0)
            {
                _hitPoints--;
                UpdateBlockSprite();
            }

            if (_hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Private methods

        private void UpdateBlockSprite()
        {
            if (_hitPoints == 2)
            {
                _spriteRenderer.sprite = _crackedBlock;
            }

            if (_hitPoints == 1)
            {
                _spriteRenderer.sprite = _heavilyCrackedBlock;
            }
        }

        #endregion
    }
}