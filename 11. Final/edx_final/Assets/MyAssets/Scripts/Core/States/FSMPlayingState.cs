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

        public override void OnStateUpdate()
        {
            
        }

        public override void OnStateExit()
        {
            
        }

        public override void ReceiveEvent(FSMStateEvent stateEvent)
        {
            switch (stateEvent)
            {
                default:
                    break;
            }
        }
    }
}
