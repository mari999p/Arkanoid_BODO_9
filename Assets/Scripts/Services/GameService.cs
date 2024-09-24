using System;
using Arkanoid.UI;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Services
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables

        [Header("Auto Play")]
        [SerializeField] private bool _isAutoPlay;

        [Header("Settings")]
        [SerializeField] private int _maxLives = 3;

        [Header("Stats")]
        [SerializeField] private int _score;
        [SerializeField] private int _lives;

        #endregion

        #region Events

        public event Action<int> OnScoreChanged;

        #endregion

        #region Properties

        public bool IsAutoPlay => _isAutoPlay;
        public int Lives => _lives;
        public int Score => _score;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();

            _lives = _maxLives;
        }

        private void Start()
        {
            LevelService.Instance.OnAllBlocksDestroyed += AllBlocksDestroyedCallback;
        }

        private void OnDestroy()
        {
            LevelService.Instance.OnAllBlocksDestroyed -= AllBlocksDestroyedCallback;
        }

        #endregion

        #region Public methods

        public void AddLife(int value)
        {
            _lives += value;
            if (_lives > _maxLives)
            {
                _lives = _maxLives;
            }
        }

        public void AddScore(int value)
        {
            _score += value;
            OnScoreChanged?.Invoke(_score);
        }

        public void RemoveLife()
        {
            if (_lives > 0)
            {
                _lives--;
                GameOverScreen.Instance.RemoveHeart();

                LevelService.Instance.Ball.ResetBall();
                return;
            }

            Debug.LogError("GAME OVER!");
            GameOverScreen.Instance.ShowGameOver();
        }

        #endregion

        #region Private methods

        private void AllBlocksDestroyedCallback()
        {
            if (SceneLoaderService.Instance.HasNextLevel())
            {
                SceneLoaderService.Instance.LoadNextLevel();
            }
            else
            {
                Debug.LogError("GAME WIN!");
            }
        }

        #endregion
    }
}