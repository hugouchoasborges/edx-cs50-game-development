using core.states;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace core.fsm.states
{
    public class FSMGameOverMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            // Show Pause Menu
            ApplicationController.Instance.MenuController.SetGameOverMenuVisible(true, GameController.Instance.Score);

            // Pause Game
            Time.timeScale = 0;
        }

        public override void OnStateExit()
        {
            // Hide Pause Menu
            ApplicationController.Instance.MenuController.SetGameOverMenuVisible(false);

            // Resume Game
            Time.timeScale = 1;
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            switch (stateEvent)
            {
                case FSMStateEvent.MENU_PLAY_TRIGGERED:
                    SceneManager.UnloadSceneAsync(Constants.SCENE_GAME_IDX);
                    ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPreparePlayingState);
                    break;

                case FSMStateEvent.MENU_EXIT_TRIGGERED:
                    SceneManager.UnloadSceneAsync(Constants.SCENE_GAME_IDX);
                    ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPrepareMainMenuState);
                    break;
                default:
                    break;
            }
        }
    }
}
