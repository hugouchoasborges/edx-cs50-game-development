using behaviors.shooting;
using UnityEngine;

namespace behaviors
{
    public abstract class SpaceshipController: MonoBehaviour
    {
        // ========================== Components ============================

        protected Camera mCamera;

        private void Awake()
        {
            mCamera = Camera.main;
        }

        [Header("Components")]
        [SerializeField] protected SpriteRenderer mSpriteRenderer;

        [Header("Input & Behaviors")]
        [SerializeField] protected SpaceshipInput mInput;
        [SerializeField] protected MovementBehavior mMovement;
        [SerializeField] protected ShootingManager mShooting;


        // ========================== Init ============================

        protected virtual void OnEnable()
        {
            mInput.OnMoveDelegate += mMovement.Move;
            mInput.OnStopDelegate += mMovement.Stop;
            mInput.OnFirePressedDelegate += mShooting.Fire;
            mInput.OnFireHeldDelegate += mShooting.ChargeFire;
        }

        protected virtual void OnDisable()
        {
            mInput.OnMoveDelegate -= mMovement.Move;
            mInput.OnStopDelegate -= mMovement.Stop;
            mInput.OnFirePressedDelegate -= mShooting.Fire;
            mInput.OnFireHeldDelegate -= mShooting.ChargeFire;
        }
    }
}
