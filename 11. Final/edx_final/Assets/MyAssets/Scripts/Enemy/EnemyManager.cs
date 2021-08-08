using helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform[] _spawnPoints;


        private const int ENEMIES_POOL_SIZE = 10;
        private Pool<EnemyController> _enemies;
        private Pool<ExplosionParticles> _enemyExplosion;

        private void Awake()
        {
            if (_prefab != null)
                _enemies = new Pool<EnemyController>(ENEMIES_POOL_SIZE, _prefab);
            else
                _enemies = new Pool<EnemyController>(ENEMIES_POOL_SIZE, Constants.PREFAB_ENEMY);

            // Initiate its explosion particles
            _enemyExplosion = new Pool<ExplosionParticles>(ENEMIES_POOL_SIZE, Constants.PREFAB_ENEMY_EXPLOSION);
        }


        // ========================== Spawn ============================

        public void SpawnEnemies()
        {
            StartCoroutine(Coroutine_SpawnEnemies());
        }

        private void SpawnEnemy()
        {
            if (_enemies.InstancesCount > 0)
                return;

            EnemyController enemy = _enemies.Instantiate(transform);
            enemy.onDestroy += OnEnemyDestroyed;
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            enemy.Init();
        }

        private WaitForSeconds _spawnEnemiesDelay = new WaitForSeconds(2f);
        private IEnumerator Coroutine_SpawnEnemies()
        {
            while (true)
            {
                SpawnEnemy();
                yield return _spawnEnemiesDelay;
            }
        }


        // ========================== Destroy ============================

        private void OnEnemyDestroyed(EnemyController enemy)
        {
            _enemyExplosion.Instantiate(transform)?.Play(enemy.BodyPosition, (obj) => _enemyExplosion.Destroy(obj));

            enemy.onDestroy -= OnEnemyDestroyed;
            _enemies.Destroy(enemy);
        }
    }
}