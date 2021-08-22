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

        [SerializeField] private Canvas _canvas;
        [SerializeField] private UnityEngine.UI.CanvasScaler _canvasScaler;

        public PlayerController player;

        [SerializeField] private BackgroundController _background;
        public BackgroundController Background => _background;

        [SerializeField] private PropManager _propManager;
        public PropManager PropManager => _propManager;

        [SerializeField] private EnemyManager _enemyManager;
        public EnemyManager EnemyManager => _enemyManager;

        [SerializeField] private EnemyWaveController _enemyWaveController;
        public EnemyWaveController EnemyWaveController => _enemyWaveController;

        [SerializeField] private MenuController _menuController;
        public MenuController MenuController => _menuController;

        public FSM fsm { get; private set; }

        private void Awake()
        {
            Instance = this;

            Camera cam = Constants.Camera = Camera.main;
            Vector2 bounds = Constants.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
            Constants.ScreenBounds = new Vector2(bounds.y * _canvasScaler.referenceResolution.x / _canvasScaler.referenceResolution.y, bounds.y);
            fsm = GetComponent<FSM>();
        }

        private void Start()
        {
            InitGame();
        }


        // ========================== Init ============================

        private void InitGame()
        {
            // FSM
            fsm.GoToState(states.FSMStateType.FSMPrepareMainMenuState);
        }
    }
}