using System;
using System.Collections.Generic;
using System.Linq;
using Arkanoid.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _yOffsetFromPlatform = 1;

        [Header("Audio")]
        [SerializeField] private AudioClip _hitAudioClip;
        [SerializeField] private Object _explosionEffectPrefab;
        [SerializeField] private AudioClip _explosionAudioClip;

        private float _explosionRadius;
        private bool _isExplosive;
        private bool _isStarted;
        private Platform _platform;

        #endregion

        #region Events

        public static event Action<Ball> OnCreated;
        public static event Action<Ball> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();

            OnCreated?.Invoke(this);

            if (GameService.Instance.IsAutoPlay)
            {
                StartFlying();
            }
        }

        private void Update()
        {
            if (_isStarted)
            {
                return;
            }

            MoveWithPlatform();

            if (Input.GetMouseButtonDown(0))
            {
                StartFlying();
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            AudioService.Instance.PlaySfx(_hitAudioClip);
            if (_isExplosive && other.gameObject.CompareTag(Tag.Block))
            {
                Explode();
            }
        }

        private void OnDrawGizmos()
        {
            if (!_isStarted)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)GetRandomStartDirection());
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb.velocity);
            }
        }

        #endregion

        #region Public methods

        public void ChangeSpeed(float speedChange)
        {
            _speed += speedChange;
            _rb.velocity = _rb.velocity.normalized * _speed;
        }

        public Ball Clone()
        {
            return Instantiate(this, transform.position, Quaternion.identity);
        }

        public void MakeExplosive(float explosionRadius)
        {
            _isExplosive = true;
            _explosionRadius = explosionRadius;
        }

        public void ResetBall()
        {
            _isStarted = false;
            _rb.velocity = Vector2.zero;
        }

        #endregion

        #region Private methods

        private void Explode()
        {
            List<Block> blocksInRange = LevelService.Instance.Blocks
                .Where(block => Vector3.Distance(transform.position, block.transform.position) <= _explosionRadius)
                .ToList();

            foreach (Block block in blocksInRange)
            {
                Instantiate(_explosionEffectPrefab, block.transform.position, Quaternion.identity);
                Destroy(block.gameObject);
                GameService.Instance.AddScore(block.GetScore());
                AudioService.Instance.PlaySfx(_explosionAudioClip);
            }
        }

        private Vector2 GetRandomStartDirection()
        {
            float minAngleDeg = -75f;
            float maxAngleDeg = 75f;  
            float minAngleRad = minAngleDeg * Mathf.Deg2Rad;
            float maxAngleRad = maxAngleDeg * Mathf.Deg2Rad;
            float randomAngle = Random.Range(minAngleRad, maxAngleRad);
            Vector2 direction = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;

            return direction;

        }

        private void MoveWithPlatform()
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = _platform.transform.position.x;
            currentPosition.y = _platform.transform.position.y + _yOffsetFromPlatform;
            transform.position = currentPosition;
        }

        private void StartFlying()
        {
            _isStarted = true;
            Vector2 randomDirection = GetRandomStartDirection();
            _rb.velocity = randomDirection * _speed;
        }

        #endregion
    }
}