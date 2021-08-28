using core;
using UnityEngine;

namespace menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _panelMainMenu;
        [SerializeField] private GameObject _panelMainMenuDefaultSelected;

        [SerializeField] private GameObject _panelPauseMenu;
        [SerializeField] private GameObject _panelPauseMenuDefaultSelected;

        [SerializeField] private GameObject _panelGameOverMenu;
        [SerializeField] private GameObject _panelGameOverMenuDefaultSelected;


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

        public void SetGameOverMenuVisible(bool visible)
        {
            _panelGameOverMenu.SetActive(visible);
            ApplicationController.Instance.FocusController.SetFocusToObject(_panelGameOverMenuDefaultSelected);
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
    }
}
