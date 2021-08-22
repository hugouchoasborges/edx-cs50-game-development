using core.fsm.states;
using core.states;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace core
{
    public class FSM : MonoBehaviour
    {
        public IFSMState currentState { get; private set; }
        private Dictionary<string, IFSMState> _states = new Dictionary<string, IFSMState>();

        public readonly Type[] STATE_TYPES = new Type[]
        {
            typeof(FSMPrepareMainMenuState),
            typeof(FSMMainMenuState),
            typeof(FSMPreparePlayingState),
            typeof(FSMPlayingState),
        };


        // ========================== Init ============================


        private void Start()
        {
            CreateStates();
        }

        private void CreateStates()
        {
            foreach (Type stateType in STATE_TYPES)
            {
                _states.Add(stateType.Name, Activator.CreateInstance(stateType) as IFSMState);
            }
        }


        // ========================== States Methods ============================


        public void GoToState(FSMStateType stateType)
        {
            // exit current State
            currentState?.OnStateExit();
            currentState = _states[stateType.ToString()];
            currentState?.OnStateEnter();
        }

        public void DispatchEvent(FSMStateEvent stateEvent)
        {
            currentState?.ReceiveEvent(stateEvent);
        }

        private void Update()
        {
            currentState?.OnStateUpdate();
        }
    }
}
