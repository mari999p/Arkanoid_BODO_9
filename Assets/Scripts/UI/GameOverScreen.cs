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
        [SerializeField] private AudioClip _explosionAudioClip;

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

        public void ShowGameOver()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                _scoreLabel.text = $"Game Over!\n Score: {GameService.Instance.Score}";
                AudioService.Instance.PlaySfx(_explosionAudioClip);
                PauseService.Instance.TogglePause();
            }
        }

        #endregion

        #region Private methods

        private void UpdateScoreLabel(int score)
        {
            _scoreLabel.text = $"Score: {score}";
        }

        #endregion
    }
}