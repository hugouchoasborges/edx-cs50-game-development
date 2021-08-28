using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMPlayingState : IFSMState
    {
        public override void OnStateEnter()
        {
            // Show HUD
            ApplicationController.Instance.MenuController.SetHUDMenuVisible(true);

            // Start spawning props
            GameController.Instance.PropManager.SpawnPropsLoop(5);

            // Start spawning waves of enemies
            GameController.Instance.EnemyWaveController.StartFirstWave();
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            base.ReceiveEvent(stateEvent);
            switch (stateEvent)
            {
                case FSMStateEvent.PLAYER_ON_DEATH:
                    GameController.Instance.OnPlayerDeath();
                    break;
                case FSMStateEvent.GAME_OVER:
                    ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMGameOverMenuState);
                    break;
                case FSMStateEvent.RESPAWN_PLAYER:
                    GameController.Instance.StartRespawnSequence();
                    break;
            }
        }

        public override void OnStateUpdate()
        {
            // Check Pause Input
            if (Input.GetButtonDown("Pause"))
            {
                ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPauseMenuState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            // Hide HUD
            ApplicationController.Instance.MenuController.SetHUDMenuVisible(true);
        }
    }
}
