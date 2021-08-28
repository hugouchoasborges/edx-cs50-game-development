using behaviors;
using behaviors.shooting;
using core;
using helpers;
using System;
using UnityEngine;
using util;

namespace enemy
{
    public class EnemyController : SpaceshipController, IPoolable
    {

        public event Action<EnemyController> onDestroy = delegate { };

        // Collision handling
        [SerializeField] private GameObject _colliderHolder;

        public Vector2 BodyPosition => mSpriteRenderer.transform.position;

        // ========================== Init ============================

        public void Init()
        {
            // Enemy Input
            mInput.EnableInput(true);

            // Default weapon
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.LEFT);
            //mShooting.InstallWeapon(ShootingManager.WeaponPosition.CENTER);
            mShooting.InstallWeapon(ShootingManager.WeaponPosition.RIGHT);

            // Add the collider listener to the child object with the Collider2D
            EnemyCollider collider = _colliderHolder.AddComponent<EnemyCollider>();
            collider.onCollisionEnter2D = MyOnCollisionEnter2D;

        }

        private void MyOnCollisionEnter2D(Collision2D collision)
        {
            GameController.Instance.OnEnemyDestroyed();
            onDestroy(this);
        }


        // ========================== Animator methods ============================

        public void Animator_Shoot()
        {
            mShooting.Fire();
        }
    }


    // ----------------------------------------------------------------------------------
    // ========================== External collider handling ============================
    // ----------------------------------------------------------------------------------


    [RequireComponent(typeof(Collider2D))]
    public class EnemyCollider: MonoBehaviour
    {
        public Action<Collision2D> onCollisionEnter2D;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionEnter2D(collision);
        }
    }
}