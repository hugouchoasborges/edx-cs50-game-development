using behaviors;
using behaviors.shooting;
using collectables;
using core;
using enemy;
using UnityEngine;
using util;

namespace player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : SpaceshipController
    {
        [SerializeField] private ScreenBoundariesBehavior _screenBoundariesBehavior;

        [SerializeField] private LayerMask _whatIsEnemy;

        [SerializeField] private LayerMask _whatIsCollectable;

        protected override void Awake()
        {
            base.Awake();
        }

        // ========================== Init ============================

        public void Init()
        {
            // Screen bounds
            Vector3 playerSize = mSpriteRenderer.bounds.size;

            _screenBoundariesBehavior.SetBorders(
                new Rect(
                    0, // x
                    0, // y
                    Constants.ScreenBounds.x, // width
                    Constants.ScreenBounds.y // height
                    ),
                playerSize
                );

            // Player Input
            mInput.EnableInput(true);

            // Default weapon
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.LEFT);
            //mShooting.InstallWeapon(ShootingManager.WeaponPosition.CENTER);
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.RIGHT);
        }


        // ========================== Stats ============================

        [Header("Statistics")]
        [SerializeField] private int _collectables = 0;

        // ========================== Collision ============================

        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCollision(collision.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.gameObject);
        }

        private void HandleCollision(GameObject collision)
        {
            if(ApplicationController.LayerMaskContains(_whatIsCollectable, collision.layer))
            {
                OnCollectableCollided();
            }
            else if (ApplicationController.LayerMaskContains(_whatIsEnemy, collision.layer))
            {
                OnEnemyCollided();
            }
        }

        private void OnCollectableCollided()
        {
            // Collect it
            _collectables++;
        }

        private void OnEnemyCollided()
        {
            ApplicationController.Instance.fsm.DispatchEvent(core.states.FSMStateEvent.PLAYER_ON_DEATH);
        }

    }
}