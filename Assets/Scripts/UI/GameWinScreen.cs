using Arkanoid.Services;
using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class VictoryScreen : MonoBehaviour
    {
        #region Variables

        public static VictoryScreen Instance;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private GameObject _victoryPanel;

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
                TMP_Text victoryText = _victoryPanel.GetComponentInChildren<TMP_Text>();
                victoryText.text = $"Victory!\nScore: {GameService.Instance.Score}";
                Time.timeScale = 0;
            }
        }

        #endregion
    }
}