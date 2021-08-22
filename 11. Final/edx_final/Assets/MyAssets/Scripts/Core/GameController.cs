using background;
using enemy;
using menu;
using player;
using props;
using UnityEngine;
using util;

namespace core
{
    public class GameController : MonoBehaviour
    {
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

        [HideInInspector] public PlayerController player;

        [SerializeField] private PropManager _propManager;
        public PropManager PropManager => _propManager;

        [SerializeField] private EnemyManager _enemyManager;
        public EnemyManager EnemyManager => _enemyManager;

        [SerializeField] private EnemyWaveController _enemyWaveController;
        public EnemyWaveController EnemyWaveController => _enemyWaveController;

        private void Awake()
        {
            Instance = this;
        }
    }
}