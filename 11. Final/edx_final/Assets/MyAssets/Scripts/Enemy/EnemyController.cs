using behaviors;
using behaviors.shooting;
using helpers;
using UnityEngine;

namespace enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyController : SpaceshipController, IPoolable
    {
        // ========================== Init ============================

        public void Init()
        {
            // Enemy Input
            mInput.EnableInput(true);

            // Default weapon
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.LEFT);
            //mShooting.InstallWeapon(ShootingManager.WeaponPosition.CENTER);
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.RIGHT);
        }
    }
}