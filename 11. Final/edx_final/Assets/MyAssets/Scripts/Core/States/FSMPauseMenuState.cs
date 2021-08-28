using core.states;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace core.fsm.states
{
    public class FSMPauseMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            // Show Pause Menu
            ApplicationController.Instance.MenuController.SetPauseMenuVisible(true);

            // Pause Game
            Time.timeScale = 0;
        }

        public override void OnStateUpdate()
        {
            // Check Pause Input
            if (Input.GetButtonDown("Pause"))
            {
                ResumeToGame();
            }
        }

        private void ResumeToGame()
        {
            ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPlayingState);
        }

        private void GoToMainMenu()
        {
            SceneManager.UnloadSceneAsync(Constants.SCENE_GAME_IDX);
            ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPrepareMainMenuState);
        }

        public override void OnStateExit()
        {
            // Hide Pause Menu
            ApplicationController.Instance.MenuController.SetPauseMenuVisible(false);

            // Resume Game
            Time.timeScale = 1;
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            switch (stateEvent)
            {
                case FSMStateEvent.MENU_RESUME_TRIGGERED:
                    ResumeToGame();
                    break;

                case FSMStateEvent.MENU_EXIT_TRIGGERED:
                    GoToMainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
