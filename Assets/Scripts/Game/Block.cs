using System;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [Header("Settings Sprite")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _crackedBlock;
        [SerializeField] private Sprite _heavilyCrackedBlock;

        [Header("Block Settings")]
        [SerializeField] private int _hitPoints = 3;
        [SerializeField] private int _score = 1;
        [SerializeField] private bool _isInvisible;

        [Header("Block Settings")]
        [SerializeField] private bool _isExplosive;
        [SerializeField] private float _explosiveRadius = 1f;
        [SerializeField] private LayerMask _explosiveLayerMask;
        [SerializeField] private GameObject _explosionVfxPrefab;

        [Header("Audio")]
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _spriteRenderer.enabled = !_isInvisible;
            OnCreated?.Invoke(this);
        }

        private void OnDestroy()
        {
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
                DestroyBlock();
            }

            if (!_isInvisible)
            {
                return;
            }

            _spriteRenderer.enabled = true;
        }

        private void OnDrawGizmos()
        {
            if (_isExplosive)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _explosiveRadius);
            }
        }

        #endregion

        #region Public methods

        public void ForceDestroy()
        {
            DestroyBlock();
        }

        #endregion

        #region Private methods

        private void DestroyBlock()
        {
            GameService.Instance.AddScore(_score);
            PickUpService.Instance.SpawnPickUp(transform.position);
            Destroy(gameObject);
            Explode();
        }

        private void Explode()
        {
            if (!_isExplosive)
            {
                return;
            }

            AudioService.Instance.PlaySfx(_explosionAudioClip);

            if (_explosionVfxPrefab != null)
            {
                Instantiate(_explosionVfxPrefab, transform.position, Quaternion.identity);
            }

            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(transform.position, _explosiveRadius, _explosiveLayerMask);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.TryGetComponent(out Block block))
                {
                    block.ForceDestroy();
                }
            }
        }

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