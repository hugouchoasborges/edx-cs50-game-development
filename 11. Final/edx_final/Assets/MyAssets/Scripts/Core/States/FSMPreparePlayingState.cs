using core.states;
using player;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace core.fsm.states
{
    public class FSMPreparePlayingState : IFSMState
    {
        public override void OnStateEnter()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(Constants.SCENE_GAME_IDX, LoadSceneMode.Additive);
            op.completed += OnGameSceneLoaded;

        }

        private void OnGameSceneLoaded(AsyncOperation op)
        {
            op.completed -= OnGameSceneLoaded;

            // Set the new Scene as the default, so new instantiated objects can be placed at it by default
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(Constants.SCENE_GAME_IDX));

            GameController.Instance.player = (GameObject.Instantiate(Resources.Load(Constants.PREFAB_PLAYER)) as GameObject).GetComponent<PlayerController>();
            ApplicationController.Instance.fsm.GoToState(FSMStateType.FSMPlayingState);
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
