using Arkanoid.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Services
{
    public class PauseService : SingletonMonoBehaviour<PauseService>
    {
        #region Variables

        [SerializeField] private GameObject _pauseMenuPanel;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private bool _isPaused;

        #endregion

        #region Properties

        public bool IsPaused => _isPaused;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            if (_pauseMenuPanel != null)
            {
                _pauseMenuPanel.SetActive(false);
            }

            if (_continueButton != null)
            {
                _continueButton.onClick.AddListener(ContinueGame);
            }

            if (_exitButton != null)
            {
                _exitButton.onClick.AddListener(ExitGame);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        #endregion

        #region Private methods

        private void ContinueGame()
        {
            TogglePause();
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
            if (_pauseMenuPanel != null)
            {
                _pauseMenuPanel.SetActive(_isPaused);
            }
        }

        #endregion
    }
}