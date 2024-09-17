using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector2 _startDirection;
        [SerializeField] private float _speed = 30;

        private bool _isStarted;
        private Platform _platform;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();
            GameService.Instance.OnLifeLost += HandleLifeLost;
        }

        private void Update()
        {
            if (_isStarted)
            {
                return;
            }

            MoveWithPlatform();
            if (Input.GetMouseButton(0))
            {
                StartFlying();
            }
        }

        private void OnDestroy()
        {
            GameService.Instance.OnLifeLost -= HandleLifeLost;
        }

        private void OnDrawGizmos()
        {
            if (!_isStarted)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_startDirection);
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)(_rb?.velocity ?? Vector2.zero));
            }
        }

        #endregion

        #region Private methods

        private void HandleLifeLost(int i)
        {
            _isStarted = false;
            if (_rb != null)
            {
                _rb.velocity = Vector2.zero;
            }

            MoveWithPlatform();
            UIService.Instance.RemoveHeart();
        }

        private void MoveWithPlatform()
        {
            if (_platform != null)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.x = _platform.transform.position.x;
                transform.position = currentPosition;
            }
        }

        private void StartFlying()
        {
            _isStarted = true;
            if (_rb != null)
            {
                _rb.velocity = _startDirection.normalized * _speed;
            }
        }

        #endregion
    }
}