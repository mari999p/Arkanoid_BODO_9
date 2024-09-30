using Arkanoid.Services;
using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class GameScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private GameObject[] _hearts;
        

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            GameService.Instance.OnScoreChanged += ScoreChangedCallback;
            GameService.Instance.OnLiveChanged += UpdateHearts;
            UpdateHearts(GameService.Instance.Lives); 
            UpdateScore();
        }

        private void UpdateHearts(int lives)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                _hearts[i].SetActive(i < lives);
            }
        }

        private void OnDestroy()
        {
            GameService.Instance.OnScoreChanged -= ScoreChangedCallback;
            GameService.Instance.OnLiveChanged -= UpdateHearts;
        }

        #endregion

        #region Private methods

        private void ScoreChangedCallback(int score)
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            _scoreLabel.text = $"Score: {GameService.Instance.Score}";
        }
       

        #endregion
    }
}