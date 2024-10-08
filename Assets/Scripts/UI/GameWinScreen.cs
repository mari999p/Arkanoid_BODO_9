using Arkanoid.Services;
using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class GameWinScreen : MonoBehaviour
    {
        #region Variables

        public static GameWinScreen Instance;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _gameWinLabel;
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private AudioClip _explosionAudioClip;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Public methods

        public void ShowVictory()
        {
            if (_victoryPanel)
            {
                _victoryPanel.SetActive(true);
                _gameWinLabel.text = $"Game Win!\nScore: {GameService.Instance.Score}";
                Time.timeScale = 0;
                AudioService.Instance.PlaySfx(_explosionAudioClip);
                PauseService.Instance.TogglePause();
            }
        }

        #endregion
    }
}