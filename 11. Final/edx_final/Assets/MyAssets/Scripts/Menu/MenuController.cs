using core;
using UnityEngine;

namespace menu
{
    public class MenuController: MonoBehaviour
    {
        [SerializeField] private GameObject _panelMainMenu;


        // ========================== UI Stuff ============================

        public void ShowMainMenu()
        {
            _panelMainMenu.SetActive(true);
        }

        public void HideMainMenu()
        {
            _panelMainMenu.SetActive(false);
        }

        // ========================== Buttons ============================

        public void Play()
        {
            GameController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.MENU_PLAY_TRIGGERED);
        }

        public void Exit()
        {
            GameController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.MENU_EXIT_TRIGGERED);
        }
    }
}
