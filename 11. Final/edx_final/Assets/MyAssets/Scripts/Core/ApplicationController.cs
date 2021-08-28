using background;
using menu;
using UnityEngine;
using util;

namespace core
{
    public class ApplicationController : MonoBehaviour
    {
        private static ApplicationController _instance;
        public static ApplicationController Instance
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

        [SerializeField] private BackgroundController _background;
        public BackgroundController Background => _background;

        [SerializeField] private MenuController _menuController;
        public MenuController MenuController => _menuController;

        [SerializeField] private FocusController _focusController;
        public FocusController FocusController => _focusController;

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
            InitApplication();
        }


        // ========================== Init ============================

        private void InitApplication()
        {
            // FSM
            fsm.GoToState(states.FSMStateType.FSMPrepareMainMenuState);
        }


        // ========================== Auxiliar Methods ============================

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
#else
            Application.Quit();
#endif
        }

        public static bool LayerMaskContains(int layerMask, int layer)
        {
            return ((layerMask & (1 << layer)) != 0);
        }
    }
}