using core.states;
using player;
using UnityEngine;
using util;

namespace core.fsm.states
{
    public class FSMPreparePlayingState : IFSMState
    {
        public override void OnStateEnter()
        {
            GameController.Instance.player = (GameObject.Instantiate(Resources.Load(Constants.PREFAB_PLAYER)) as GameObject).GetComponent<PlayerController>();
            GameController.Instance.fsm.GoToState(FSMStateType.FSMPlayingState);
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
