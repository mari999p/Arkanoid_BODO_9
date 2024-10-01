using Arkanoid.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Arkanoid.UI
{
    public class MenuScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _startButton;
        [SerializeField] private Button[] _levelButtons;
        [SerializeField] private Color _highlightColor = Color.yellow;
        [SerializeField] private AudioClip _explosionAudioClip;
        private Color _defaultColor;
        private int _selectedLevel = -1;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _defaultColor = _startButton.image.color;
            _startButton.onClick.AddListener(StartButtonClickedCallback);
            AddEventTrigger(_startButton);
            foreach (Button button in _levelButtons)
            {
                button.onClick.AddListener(() => LevelButtonClicked(button));

                AddEventTrigger(button);
            }
        }

        #endregion

        #region Private methods

        private void AddEventTrigger(Button button)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = button.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entryEnter = new()
            {
                eventID = EventTriggerType.PointerEnter,
            };
            entryEnter.callback.AddListener(_ => { OnPointerEnter(button); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new()
            {
                eventID = EventTriggerType.PointerExit,
            };
            entryExit.callback.AddListener(_ => { OnPointerExit(button); });
            trigger.triggers.Add(entryExit);
        }

        private void OnPointerEnter(Button button)
        {
            button.image.color = _highlightColor;
            AudioService.Instance.PlaySfx(_explosionAudioClip);
        }

        private void OnPointerExit(Button button)
        {
            if (_selectedLevel == System.Array.IndexOf(_levelButtons, button))
            {
                return;
            }

            button.image.color = _defaultColor;
        }

        private void StartButtonClickedCallback()
        {
            if (_selectedLevel >= 0)
            {
                SceneLoaderService.Instance.LoadLevel(_selectedLevel);
            }
        }

        private void LevelButtonClicked(Button button)
        {
            _selectedLevel = System.Array.IndexOf(_levelButtons, button);
            Debug.Log("Selected level: " + _selectedLevel);
            foreach (Button btn in _levelButtons)
            {
                btn.image.color = _defaultColor;
            }

            button.image.color = _highlightColor;

            #endregion
        }
    }
}