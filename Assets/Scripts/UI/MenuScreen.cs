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
        private Color _defaultColor;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _defaultColor = _startButton.image.color;
            _startButton.onClick.AddListener(StartButtonClickedCallback);
            AddEventTrigger(_startButton);
            foreach (Button button in _levelButtons)
            {
                button.onClick.AddListener(() => StartGameWithLevel(button));
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
        }

        private void OnPointerExit(Button button)
        {
            button.image.color = _defaultColor;
        }

        private void StartButtonClickedCallback()
        {
            SceneLoaderService.Instance.LoadFirstLevel();
        }

        private void StartGameWithLevel(Button button)
        {
            int levelIndex = System.Array.IndexOf(_levelButtons, button);
            SceneLoaderService.Instance.LoadLevel(levelIndex);
        }

        #endregion
    }
}