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
        [SerializeField] private AudioClip _audioClip;
       
        private int _selectedLevel = -1;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
          
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
          
            trigger.triggers.Add(entryExit);
        }

        private void OnPointerEnter(Button button)
        {
            AudioService.Instance.PlaySfx(_audioClip);
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
            
            #endregion
        }
    }
}