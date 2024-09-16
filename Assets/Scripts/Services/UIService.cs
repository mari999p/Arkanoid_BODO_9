using TMPro;
using UnityEngine;

namespace Arkanoid.Services
{
    public class UIService : MonoBehaviour
    {
        #region Variables

        public static UIService Instance;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject[] _hearts;
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _platform;
        [SerializeField] private GameObject _continueButton;
        

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameService.Instance.OnScoreChange += UpdateScoreLabel;
            GameService.Instance.OnGameOver += ShowGameOver;
            UpdateScoreLabel(GameService.Instance.Score);
        }

        #endregion

        #region Public methods

        public void RemoveHeart()
        {
            if (GameService.Instance.Lives < 3)
            {
                _hearts[GameService.Instance.Lives].SetActive(false);
            }
        }

        #endregion

        #region Private methods

        private void ShowGameOver(int i)
        {
            _gameOverPanel.SetActive(true);
            _gameOverPanel.GetComponentInChildren<TMP_Text>().text =
                $"Game Over!\n Score: {GameService.Instance.Score}";
            Time.timeScale = 0;
            _ball.SetActive(false);
            _platform.SetActive(false);
        }

        private void UpdateScoreLabel(int score)
        {
            _scoreLabel.text = $"Score: {score}";
        }
        
        #endregion
    }
    
}