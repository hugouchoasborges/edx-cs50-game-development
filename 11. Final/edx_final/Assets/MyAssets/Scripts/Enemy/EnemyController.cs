using behaviors;
using behaviors.shooting;
using helpers;
using System;
using UnityEngine;
using util;

namespace enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : SpaceshipController, IPoolable
    {

        public event Action<EnemyController> onDestroy = delegate { };

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onDestroy(this);
        }
    }
}