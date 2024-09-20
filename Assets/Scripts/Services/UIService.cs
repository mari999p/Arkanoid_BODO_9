// using TMPro;
// using UnityEngine;
//
// namespace Arkanoid.Services
// {
//     public class UIService : MonoBehaviour
//     {
//         #region Variables
//
//         public static UIService Instance;
//
//         [SerializeField] private TMP_Text _scoreLabel;
//         [SerializeField] private GameObject _gameOverPanel;
//         [SerializeField] private GameObject[] _hearts;
//         [SerializeField] private GameObject _ball;
//         [SerializeField] private GameObject _platform;
//
//         #endregion
//
//         #region Unity lifecycle
//
//         private void Awake()
//         {
//             Instance = this;
//         }
//
//         private void Start()
//         {
//             GameService.Instance.OnScoreChange += UpdateScoreLabel;
//             GameService.Instance.OnGameOver += ShowGameOver;
//             UpdateScoreLabel(GameService.Instance.Score);
//         }
//
//         #endregion
//
//         #region Public methods
//
//         public void RemoveHeart()
//         {
//             if (GameService.Instance.Lives < 3 && _hearts[GameService.Instance.Lives] != null)
//             {
//                 _hearts[GameService.Instance.Lives].SetActive(false);
//             }
//         }
//
//         #endregion
//
//         #region Private methods
//
//         private void ShowGameOver(int i)
//         {
//             if (_gameOverPanel != null)
//             {
//                 _gameOverPanel.SetActive(true);
//                 TMP_Text gameOverText = _gameOverPanel.GetComponentInChildren<TMP_Text>();
//                 if (gameOverText != null)
//                 {
//                     gameOverText.text = $"Game Over!\n Score: {GameService.Instance.Score}";
//                 }
//
//                 Time.timeScale = 0;
//                 if (_ball != null)
//                 {
//                     _ball.SetActive(false);
//                 }
//
//                 if (_platform != null)
//                 {
//                     _platform.SetActive(false);
//                 }
//             }
//         }
//
//         private void UpdateScoreLabel(int score)
//         {
//             if (_scoreLabel != null)
//             {
//                 _scoreLabel.text = $"Score: {score}";
//             }
//         }
//
//         #endregion
//     }
// }