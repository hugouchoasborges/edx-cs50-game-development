using behaviors.shooting;
using System.Collections.Generic;
using UnityEngine;

namespace helpers
{
    class Pool<T> where T : class, IPoolable, new()
    {
        private int _size = 10;
        private Queue<T> _queue = new Queue<T>();

        private GameObject _prefab;
        private string _path;
        private Transform _parent;

        private static Transform _defaultPoolParent;

        public int InstancesCount => _size - _queue.Count;

        // ========================== Init ============================

        public Pool(int size, string path, Transform parent = null)
        {
            _parent = parent;
            if (_parent == null)
                _parent = GetDefaultPoolParent();
            _path = path;
            _size = size;

            _prefab = Resources.Load<GameObject>(_path);
            InitPool();
        }

        public Pool(int size, GameObject prefab, Transform parent = null)
        {
            _parent = parent;
            if (_parent == null)
                _parent = GetDefaultPoolParent();
            _size = size;

            _prefab = prefab;
            InitPool();
        }

        public void InitPool()
        {
            // Init bullet Queue
            for (int i = 0; i < _size; i++)
            {
                _queue.Enqueue(GameObject.Instantiate<GameObject>(_prefab, _parent).GetComponent<T>());
            }
        }

        private Transform GetDefaultPoolParent()
        {
            if (_defaultPoolParent != null)
                return _defaultPoolParent;

            string poolName = $"{typeof(T).Name}_Pool";

            GameObject obj = GameObject.Find(poolName);
            if (obj == null)
            {
                obj = new GameObject(poolName);
                obj.SetActive(false);
            }

            _defaultPoolParent = obj.transform;

            return _defaultPoolParent;
        }

        // ========================== Instance ============================

        public T Instantiate(Transform parent = null)
        {
            T t = _queue.Count > 0
                ? _queue.Dequeue()
                : GameObject.Instantiate<GameObject>(_prefab, _parent).GetComponent<T>();

            if (parent != null) t.gameObject.transform.SetParent(parent, false);
            t.gameObject.transform.position = Vector3.zero;

            return t;
        }

        public void Destroy(T t)
        {
            _queue.Enqueue(t);
            t.gameObject.transform.SetParent(_parent, false);
        }
    }
}
