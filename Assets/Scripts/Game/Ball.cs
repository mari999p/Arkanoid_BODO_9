using System;
using Arkanoid.Services;
using Arkanoid.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TrailRenderer _trailRenderer;
        
        [Header("Settings")]
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _yOffsetFromPlatform = 1;

        [Header("Audio")]
        [SerializeField] private AudioClip _hitAudioClip;
        [SerializeField] private AudioClip _explosionAudioClip;

        [Header("Effects")]
        [SerializeField] private Object _explosionEffectPrefab;
        
        [Header("Direction")]
        [SerializeField] private float _directionMin = -90;
        [SerializeField] private float _directionMax = 90;
        [SerializeField] private int _segments = 10;

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
                GizmosUtils.DrawArc2D(transform.position, Vector2.up, _directionMin, _directionMax, _speed, _segments);
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
            Ball clone = Instantiate(this, transform.position, Quaternion.identity);
            clone._isStarted = _isStarted;
            clone._rb.velocity = _rb.velocity;
            return clone;
        }

        public Vector2 GetRandomStartDirection()
        {
            float minAngleDeg = -75f;
            float maxAngleDeg = 75f;
            float minAngleRad = minAngleDeg * Mathf.Deg2Rad;
            float maxAngleRad = maxAngleDeg * Mathf.Deg2Rad;
            float randomAngle = Random.Range(minAngleRad, maxAngleRad);
            Vector2 direction = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;

            return direction;
        }

        public void MakeExplosive(float explosionRadius, Sprite sprite, Gradient trailGradient)
        {
            _isExplosive = true;
            _explosionRadius = explosionRadius;
            _spriteRenderer.sprite = sprite;
            _trailRenderer.colorGradient = trailGradient;
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
            foreach (Collider2D collider1 in colliders)
            {
                Block block = collider1.GetComponent<Block>();
                if (block != null)
                {
                    Instantiate(_explosionEffectPrefab, block.transform.position, Quaternion.identity);
                    AudioService.Instance.PlaySfx(_explosionAudioClip);
                    block.ForceDestroy();
                }
            }
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