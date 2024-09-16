using System;
using Arkanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Arkanoid.Services
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables

        [SerializeField] private int _score;
        [SerializeField] private int _lives = 3;
        

        #endregion

        #region Properties

        public event Action<int> OnScoreChange; 
        public event Action<int> OnLifeLost; 
        public event Action<int> OnGameOver;
        

        public int Score => _score;
        public int Lives => _lives;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            LevelService.Instance.OnAllBlocksDestroyed += AllBlocksDestroyedCallback;
        }

        private void OnDestroy()
        {
            LevelService.Instance.OnAllBlocksDestroyed -= AllBlocksDestroyedCallback;
        }

        public void AddScore(int value)
        {
            _score += value;
            OnScoreChange?.Invoke(_score);
        }

        public void LoseLife()
        {
            _lives--;
            OnLifeLost?.Invoke(_lives);
            if (_lives <= 0)
            {
                OnGameOver?.Invoke(0);
            }
        }

        #endregion

        #region Private methods

        private void AllBlocksDestroyedCallback()
        {
            
            SceneLoaderService.LoadNextLevelTest("Level2");
        }

        #endregion
    }
}