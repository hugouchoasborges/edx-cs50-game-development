using core.states;
using UnityEngine;

namespace core.fsm.states
{
    public class FSMPrepareMainMenuState : IFSMState
    {
        public override void OnStateEnter()
        {
            ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMMainMenuState);
        }
    }
}
