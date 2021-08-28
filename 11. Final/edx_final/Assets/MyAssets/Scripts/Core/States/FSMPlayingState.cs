using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMPlayingState : IFSMState
    {
        public override void OnStateEnter()
        {
            GameController.Instance.player.Init();

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
                    ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMGameOverMenuState);
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
    }
}
