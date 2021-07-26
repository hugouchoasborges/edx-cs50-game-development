using helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace util
{
    public class ExplosionParticles : MonoBehaviour,IPoolable
    {
        [SerializeField] private ParticleSystem _explosionParticles;

        private Action<ExplosionParticles> _onDestroy;

        private void Awake()
        {
            ParticleSystem.MainModule main = _explosionParticles.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Play(Vector2 position, Action<ExplosionParticles> onDestroy)
        {
            transform.position = position;
            _onDestroy = onDestroy;
            _explosionParticles.Play();
        }

        private void OnParticleSystemStopped()
        {
            _onDestroy.Invoke(this);
        }
    }
}