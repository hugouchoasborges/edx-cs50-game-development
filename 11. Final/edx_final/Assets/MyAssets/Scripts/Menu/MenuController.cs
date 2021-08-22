using core;
using UnityEngine;

namespace menu
{
    public class MenuController: MonoBehaviour
    {
        [SerializeField] private GameObject _panelMainMenu;
        [SerializeField] private GameObject _panelPauseMenu;


        // ========================== UI Stuff ============================

        public void ShowMainMenu()
        {
            _panelMainMenu.SetActive(true);
        }

        public void HideMainMenu()
        {
            _panelMainMenu.SetActive(false);
        }

        public void ShowPauseMenu()
        {
            _panelPauseMenu.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _panelPauseMenu.SetActive(false);
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
