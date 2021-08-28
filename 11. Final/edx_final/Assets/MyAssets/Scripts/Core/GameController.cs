using background;
using enemy;
using menu;
using player;
using props;
using System.Collections;
using UnityEngine;
using util;

namespace core
{
    public class GameController : MonoBehaviour
    {

        // ========================== Singleton ============================


        private static GameController _instance;
        public static GameController Instance
        {
            get => _instance;

            private set
            {
                if (_instance != null)
                    Destroy(value);
                else
                    _instance = value;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        // ========================== References ============================


        [HideInInspector] public PlayerController player;

        [SerializeField] private PropManager _propManager;
        public PropManager PropManager => _propManager;

        [SerializeField] private EnemyManager _enemyManager;
        public EnemyManager EnemyManager => _enemyManager;

        [SerializeField] private EnemyWaveController _enemyWaveController;
        public EnemyWaveController EnemyWaveController => _enemyWaveController;


        // ========================== Player ============================

        private const int PLAYER_MAX_LIVES = 3;

        [Header("Player")]
        [SerializeField] private int _playerLives = PLAYER_MAX_LIVES;

        public void InstantiatePlayer()
        {
            player = (GameObject.Instantiate(Resources.Load(Constants.PREFAB_PLAYER)) as GameObject).GetComponent<PlayerController>();
            player.Init();
        }

        public void OnPlayerDeath()
        {
            _playerLives = Mathf.Max(0, _playerLives - 1);
            ApplicationController.Instance.MenuController.SetTotalLives(_playerLives);

            DestroyPlayer();

            if (_playerLives == 0)
                ApplicationController.Instance.fsm.DispatchEvent(states.FSMStateEvent.GAME_OVER);
            else
                ApplicationController.Instance.fsm.DispatchEvent(states.FSMStateEvent.RESPAWN_PLAYER);
        }

        private void DestroyPlayer()
        {
            // Play destroy player animation (particles...)
            Destroy(player.gameObject);
        }

        public void StartRespawnSequence()
        {
            // Spawn a new player
            // It should be Invincible for some seconds
            StartCoroutine(SpawnSequence_Coroutine());
        }

        private IEnumerator SpawnSequence_Coroutine()
        {
            yield return new WaitForSeconds(1);
            InstantiatePlayer();
        }


        // ========================== Enemies ============================

        public void OnEnemyDestroyed()
        {
            Score++;
        }


        // ========================== GameFlow ============================

        //[Header("GameFlow")]
        private int _score = 0;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ApplicationController.Instance.MenuController.SetScore(_score);
            }
        }


        public void StartGame()
        {
            Score = 0;

            InstantiatePlayer();
            _playerLives = PLAYER_MAX_LIVES;
            ApplicationController.Instance.MenuController.SetTotalLives(_playerLives);
        }
    }
}