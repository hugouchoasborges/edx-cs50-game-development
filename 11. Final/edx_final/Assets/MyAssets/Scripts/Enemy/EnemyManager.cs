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

        private Pool<EnemyController> _enemies;

        private void Awake()
        {
            if (_prefab != null)
                _enemies = new Pool<EnemyController>(10, _prefab);
            else
                _enemies = new Pool<EnemyController>(10, Constants.PREFAB_ENEMY);
        }


        // ========================== Spawn ============================

        public void SpawnEnemies()
        {
            for (int i = 0; i < 1; i++)
            {
                EnemyController enemy = _enemies.Instantiate(transform);
                //enemy.transform.position = _spawnPoints[i].position;
                enemy.transform.position = new Vector2(0, 1);
                enemy.Init();
            }
        }
    }
}