using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMMainMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            // Start animating the Background
            GameController.Instance.Background.Init();
        }

        public override void OnStateUpdate()
        {

        }

        public override void OnStateExit()
        {
            GameController.Instance.MenuController.HideMainMenu();
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            switch (stateEvent)
            {
                case FSMStateEvent.MENU_PLAY_TRIGGERED:
                    GameController.Instance.fsm.GoToState(FSMStateType.FSMPreparePlayingState);
                    break;

                case FSMStateEvent.MENU_EXIT_TRIGGERED:
                    ExitGame();
                    break;
                default:
                    break;
            }
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
#else
            Application.Quit();
#endif
        }
    }
}
