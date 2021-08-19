using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace enemy
{
    class EnemyWaveController : MonoBehaviour
    {


        // ========================== Components ============================

        private EnemyManager _enemyManager;
        private Animator _animator;

        private readonly string ANIMATOR_WAVE_PREFIX = "wave_{0}_{1}";

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _enemyManager = GetComponent<EnemyManager>();
            _enemyManager.onHordeCleared = OnHordeCleared;
        }


        // ========================== Init ============================

        private int _waveIdx = -1;
        private int _hordeIdx = -1;

        private void Start()
        {
            StartFirstWave();
        }

        private void StartFirstWave()
        {
            _waveIdx = -1;
            _hordeIdx = -1;
            StartNextWave();
        }

        // ========================== Wave flow ============================

        private void StartNextWave()
        {
            _waveIdx++;
            _hordeIdx = -1;
            StartNextHorde();
        }

        private void StartNextHorde()
        {
            _hordeIdx++;
            int hordeID = Animator.StringToHash(string.Format(ANIMATOR_WAVE_PREFIX, _waveIdx, _hordeIdx));
            if (_animator.HasState(0, hordeID))
                _animator.Play(hordeID, 0);
            else
            {
                OnWaveCleared();
            }
        }

        private void OnHordeCleared()
        {
            Debug.Log("Horde Cleared");
            StartNextHorde();
        }

        private void OnWaveCleared()
        {
            if (_animator.HasState(0, Animator.StringToHash(string.Format(ANIMATOR_WAVE_PREFIX, _waveIdx + 1, 0))))
                StartNextWave();
            else
                OnLastWaveCleared();
        }

        private void OnLastWaveCleared()
        {
            Debug.Log("Last Wave Finished");

            Debug.Log("Restarting to the first wave");
            StartFirstWave();
        }
    }
}
