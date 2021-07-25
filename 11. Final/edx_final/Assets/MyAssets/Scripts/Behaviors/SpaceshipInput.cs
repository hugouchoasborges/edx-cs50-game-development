using System;
using UnityEngine;

namespace behaviors
{
    public abstract class SpaceshipInput : MonoBehaviour
    {
        // ========================== Callbacks logic ============================

        public event Action<Vector2> OnMoveDelegate = delegate { };
        public event Action OnStopDelegate = delegate { };
        public event Action OnFirePressedDelegate = delegate { };
        public event Action OnFireHeldDelegate = delegate { };

        public void OnStop() => OnStopDelegate();
        public void OnMove(Vector2 input) => OnMoveDelegate(input);
        public void OnFirePressed() => OnFirePressedDelegate();
        public void OnFireHeld() => OnFireHeldDelegate();

        // ========================== Input Logic ============================

        [SerializeField] protected bool _inputEnabled = false;

        private const string HoriInputName = "Horizontal";
        private const string VertInputName = "Vertical";

        private const string ShootInputName = "Fire";

        private Vector2 _axisInput;

        public void EnableInput(bool value) => _inputEnabled = value;

        protected void HandleInput()
        {
            // Movement
            _axisInput = new Vector2(Input.GetAxis(HoriInputName), Input.GetAxis(VertInputName));
            OnMoveDelegate(_axisInput);

            // Shooting
            if (Input.GetButtonDown(ShootInputName))
                OnFirePressedDelegate();
            else if (Input.GetButton(ShootInputName))
                OnFireHeldDelegate();
        }
    }
}