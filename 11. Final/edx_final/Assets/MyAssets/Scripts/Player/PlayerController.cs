using behaviors;
using behaviors.shooting;
using collectables;
using UnityEngine;
using util;

namespace player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : SpaceshipController
    {
        [SerializeField] private ScreenBoundariesBehavior _screenBoundariesBehavior;

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
            if(collision.gameObject.GetComponent<Collectable>() != null)
            {
                OnCollectableCollided();
            }
        }

        private void OnCollectableCollided()
        {
            // Collect it
            _collectables++;
        }

    }
}