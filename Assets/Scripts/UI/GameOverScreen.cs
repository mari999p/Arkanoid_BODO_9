using Arkanoid.Services;
using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        #region Variables

        public static GameOverScreen Instance;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject[] _hearts;
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _platform;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateScoreLabel(GameService.Instance.Score);
        }

        #endregion

        #region Public methods

        public void RemoveHeart()
        {
            if (GameService.Instance.Lives < 3 && _hearts[GameService.Instance.Lives] != null)
            {
                _hearts[GameService.Instance.Lives].SetActive(false);
            }
        }

        #endregion

        #region Private methods

        public void ShowGameOver()
        {
            if (_gameOverPanel)
            {
                _gameOverPanel.SetActive(true);
                TMP_Text gameOverText = _gameOverPanel.GetComponentInChildren<TMP_Text>();
                    gameOverText.text = $"Game Over!\n Score: {GameService.Instance.Score}";

                Time.timeScale = 0;
                    _ball.SetActive(false);
                    _platform.SetActive(false);
            }
        }

        private void UpdateScoreLabel(int score)
        {
                _scoreLabel.text = $"Score: {score}";
           
        }

        #endregion
    }
}