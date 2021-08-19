using helpers;
using System;
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

        // Animations
        private readonly char[] _animationsSeparator = { ',' };
        private const int ANIMATOR_MOVEMENT_LAYER = 0;
        private const int ANIMATOR_SHOOTING_LAYER = 1;

        // Wave
        private Dictionary<string, List<EnemyController>> _hordeEnemiesDict = new Dictionary<string, List<EnemyController>>();
        public Action onHordeCleared;

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

        public EnemyController SpawnEnemy()
        {
            EnemyController enemy = _enemies.Instantiate(transform);
            enemy.transform.position = Vector2.one * 10;
            enemy.onDestroy += OnEnemyDestroyed;
            //enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            enemy.Init();

            return enemy;
        }


        public EnemyController Animator_SpawnEnemy(string parameters)
        {
            EnemyController enemy = SpawnEnemy();

            string[] splitParameters = parameters.Split(_animationsSeparator);
            string waveKey = splitParameters.Length > 0 ? splitParameters[0] : "";
            string moveAnimation = splitParameters.Length > 1 ? splitParameters[1] : "";
            string shootAnimation = splitParameters.Length > 2 ? splitParameters[2] : "";

            if (!_hordeEnemiesDict.ContainsKey(waveKey))
                _hordeEnemiesDict.Add(waveKey, new List<EnemyController>());

            _hordeEnemiesDict[waveKey].Add(enemy);

            if (!string.IsNullOrEmpty(moveAnimation))
                enemy.GetComponent<Animator>().Play(moveAnimation, ANIMATOR_MOVEMENT_LAYER);

            if (!string.IsNullOrEmpty(shootAnimation))
                enemy.GetComponent<Animator>().Play(shootAnimation, ANIMATOR_SHOOTING_LAYER);

            return enemy;
        }


        // ========================== Destroy ============================

        private void OnEnemyDestroyed(EnemyController enemy)
        {
            _enemyExplosion.Instantiate(transform)?.Play(enemy.BodyPosition, (obj) => _enemyExplosion.Destroy(obj));

            enemy.onDestroy -= OnEnemyDestroyed;
            _enemies.Destroy(enemy);

            foreach (var hordeEnemyList in _hordeEnemiesDict.Values)
            {
                if (hordeEnemyList.Contains(enemy))
                {
                    hordeEnemyList.Remove(enemy);
                    if (hordeEnemyList.Count == 0)
                        onHordeCleared?.Invoke();

                    break;
                }
            }
        }
    }
}