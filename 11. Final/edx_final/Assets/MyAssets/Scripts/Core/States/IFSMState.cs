using core.states;

namespace core.fsm.states
{
    public abstract class IFSMState
    {
        protected GameController gameController => GameController.Instance;

        public virtual void OnStateEnter() { }

        public virtual void OnStateUpdate() { }

        public virtual void OnStateExit() { }

        public virtual void ReceiveEvent(FSMStateEvent stateEvent) { }
    }
}
