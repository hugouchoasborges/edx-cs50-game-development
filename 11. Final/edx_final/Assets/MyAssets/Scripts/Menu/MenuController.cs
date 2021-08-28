using core;
using UnityEngine;
using UnityEngine.UI;

namespace menu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Menus' Displaying")]
        [SerializeField] private GameObject _panelMainMenu;
        [SerializeField] private GameObject _panelMainMenuDefaultSelected;

        [SerializeField] private GameObject _panelPauseMenu;
        [SerializeField] private GameObject _panelPauseMenuDefaultSelected;

        [SerializeField] private GameObject _panelGameOverMenu;
        [SerializeField] private GameObject _panelGameOverMenuDefaultSelected;
        [SerializeField] private GameObject _panelScoreMenu;
        [SerializeField] private Text _textScoreMenu;

        [SerializeField] private GameObject _panelHUDMenu;

        [Header("HUD")]
        [SerializeField] private Text _scoreText;
        [SerializeField] private CanvasGroup[] _livesCanvasGroup;


        // ========================== UI Stuff ============================

        public void SetMainMenuVisible(bool visible)
        {
            _panelMainMenu.SetActive(visible);
            ApplicationController.Instance.FocusController.SetFocusToObject(_panelMainMenuDefaultSelected);
        }

        public void SetPauseMenuVisible(bool visible)
        {
            _panelPauseMenu.SetActive(visible);
            ApplicationController.Instance.FocusController.SetFocusToObject(_panelPauseMenuDefaultSelected);
        }

        public void SetGameOverMenuVisible(bool visible, int? scoreValue = null)
        {
            _panelGameOverMenu.SetActive(visible);
            ApplicationController.Instance.FocusController.SetFocusToObject(_panelGameOverMenuDefaultSelected);

            _panelScoreMenu.SetActive(visible && scoreValue.HasValue);
            if (scoreValue.HasValue) _textScoreMenu.text = scoreValue.Value.ToString();
        }

        public void SetHUDMenuVisible(bool visible)
        {
            _panelHUDMenu.SetActive(visible);
        }

        // ========================== Buttons ============================

        public void Play()
        {
            ApplicationController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.MENU_PLAY_TRIGGERED);
        }

        public void Resume()
        {
            ApplicationController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.MENU_RESUME_TRIGGERED);
        }

        public void Exit()
        {
            ApplicationController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.MENU_EXIT_TRIGGERED);
        }


        // ========================== HUD ============================

        public void SetTotalLives(int lives)
        {
            for (int i = 0; i < _livesCanvasGroup.Length; i++)
            {
                _livesCanvasGroup[i].alpha = lives > i ? 1 : 0;
            }
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }

    }
}
