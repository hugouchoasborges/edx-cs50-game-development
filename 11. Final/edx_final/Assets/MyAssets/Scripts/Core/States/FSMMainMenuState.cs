using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMMainMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            // Show Main Menu
            ApplicationController.Instance.MenuController.ShowMainMenu();

            // Start animating the Background
            ApplicationController.Instance.Background.Init();
        }

        public override void OnStateExit()
        {
            // Hide Main Menu
            ApplicationController.Instance.MenuController.HideMainMenu();
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            switch (stateEvent)
            {
                case FSMStateEvent.MENU_PLAY_TRIGGERED:
                    ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPreparePlayingState);
                    break;

                case FSMStateEvent.MENU_EXIT_TRIGGERED:
                    ApplicationController.Instance.ExitGame();
                    break;
                default:
                    break;
            }
        }
    }
}
