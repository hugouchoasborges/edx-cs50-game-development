using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMPrepareMainMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            GameController.Instance.MenuController.ShowMainMenu();
            GameController.Instance.fsm.GoToState(FSMStateType.FSMMainMenuState);
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
