using System;
using Arkanoid.Game.PickUps;
using Arkanoid.Services;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [Header("Settings PickUp")]
        [Range(0, 100)]
        [SerializeField] private int _pickUpSpawnProbability;
        [SerializeField] private PickUp[] _possiblePickUps;

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

        #endregion

        #region Private methods

        private void DestroyBlock()
        {
            GameService.Instance.AddScore(_score);
            SpawnPickUp(transform.position);
            Destroy(gameObject);
        }

        private void SpawnPickUp(Vector3 position)
        {
            int random = Random.Range(0, 101);
            if (random <= _pickUpSpawnProbability)
            {
                int mainPickUpSpawned = Random.Range(0, 101);
                if (mainPickUpSpawned <= _pickUpSpawnProbability)
                {
                    int pickUpIndex = Random.Range(0, _possiblePickUps.Length);
                    Instantiate(_possiblePickUps[pickUpIndex], position, Quaternion.identity);
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