using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathApp
{
    public sealed class ObjectCache : SceneService
    {
        public int CachedCount { get { return all.Count; } }
        public int BorrowedCount { get { return borrowed.Count; } }

        // PRIVATE MEMBERS

        [SerializeField] bool hideCachedObjectsInHierarchy = true;
        [SerializeField] List<CacheObject> precacheObjects;

        readonly Dictionary<GameObject, Stack<GameObject>> cached = new();

        readonly Dictionary<GameObject, GameObject> borrowed = new();
        readonly List<DeferredReturn> deferred = new();
        readonly Stack<DeferredReturn> pool = new();
        readonly List<GameObject> all = new();

        // PUBLIC METHODS

        public T Get<T>(T prefab, bool activate = true, bool createIfEmpty = true) where T : Component
        {
            return Get(prefab, null, activate, createIfEmpty);
        }

        public GameObject Get(GameObject prefab, bool activate = true, bool createIfEmpty = true)
        {
            return Get(prefab, null, activate, createIfEmpty);
        }

        public T Get<T>(T prefab, Transform parent, bool activate = true, bool createIfEmpty = true)
            where T : Component
        {
            var instance = Get(prefab.gameObject, parent, activate, createIfEmpty);
            return instance != null ? instance.GetComponent<T>() : null;
        }

        public GameObject Get(GameObject prefab, Transform parent, bool activate = true, bool createIfEmpty = true)
        {
            if (cached.TryGetValue(prefab, out var stack) == false)
            {
                stack = new Stack<GameObject>();
                cached[prefab] = stack;
            }

            if (stack.Count == 0)
            {
                if (createIfEmpty)
                {
                    CreateInstance(prefab);
                }
                else
                {
                    Debug.LogWarningFormat("Prefab {0} not available in cache, returning NULL", prefab.name);
                    return null;
                }
            }

            var instance = stack.Pop();

            borrowed[instance] = prefab;

            var instanceTransform = instance.transform;

            if (parent != null)
            {
                instanceTransform.SetParent(parent, false);
            }

            instanceTransform.localPosition = Vector3.zero;
            instanceTransform.localRotation = Quaternion.identity;
            instanceTransform.localScale = Vector3.one;

            if (activate)
            {
                instance.SetActive(true);
            }

#if UNITY_EDITOR
            if (hideCachedObjectsInHierarchy)
            {
                instance.hideFlags &= ~HideFlags.HideInHierarchy;
            }
#endif
            return instance;
        }

        public void Return(Component component, bool deactivate = true)
        {
            Return(component.gameObject, deactivate);
        }

        public void Return(GameObject instance, bool deactivate = true)
        {
            if (deactivate)
            {
                instance.SetActive(false);
            }

            instance.transform.SetParent(null, false);

            cached[borrowed[instance]].Push(instance);
            borrowed.Remove(instance);

#if UNITY_EDITOR
            if (hideCachedObjectsInHierarchy)
            {
                instance.hideFlags |= HideFlags.HideInHierarchy;
            }
#endif
        }

        public void ReturnRange(List<GameObject> instances, bool deactivate = true)
        {
            for (var i = 0; i < instances.Count; i++)
            {
                Return(instances[i], deactivate);
            }
        }

        public void ReturnDeferred(GameObject instance, float delay)
        {
            var toReturn = pool.Count > 0 ? pool.Pop() : new DeferredReturn();
            toReturn.GameObject = instance;
            toReturn.Delay = delay;

            deferred.Add(toReturn);
        }

        public void Prepare(GameObject prefab, int desiredCount)
        {
            if (cached.TryGetValue(prefab, out var stack) == false)
            {
                stack = new Stack<GameObject>();
                cached[prefab] = stack;
            }

            while (stack.Count < desiredCount)
            {
                CreateInstance(prefab);
            }
        }

        // SceneService INTERFACE

        protected override void OnInitialize()
        {
            foreach (var cacheObject in precacheObjects)
            {
                cached[cacheObject.gameObject] = new Stack<GameObject>();

                for (var i = 0; i < cacheObject.count; ++i)
                {
                    CreateInstance(cacheObject.gameObject);
                }
            }
        }

        protected override void OnDeinitialize()
        {
            foreach (var item in borrowed)
            {
                var go = item.Key;
                var shouldReturn = go != null;

                foreach (var deferredItem in deferred)
                {
                    if (go == deferredItem.GameObject)
                    {
                        shouldReturn = false;
                        break;
                    }
                }

                if (shouldReturn)
                {
                    Debug.LogWarning($"Object {go.name} from cache was not returned and will be destroyed");
                }
            }

            deferred.Clear();
            borrowed.Clear();
            cached.Clear();

            foreach (var instance in all)
            {
                Destroy(instance);
            }

            all.Clear();
        }

        protected override void OnTick()
        {
            for (var i = deferred.Count; i-- > 0;)
            {
                var deferred = this.deferred[i];

                deferred.Delay -= Time.deltaTime;
                if (deferred.Delay > 0.0f)
                    continue;

                this.deferred.RemoveBySwap(i);
                Return(deferred.GameObject, true);

                deferred.Reset();
                pool.Push(deferred);
            }
        }

        // PRIVATE METHODS

        void CreateInstance(GameObject prefab)
        {
            var instance = Instantiate(prefab, null, false);
            instance.name = prefab.name;

            instance.SetActive(false);
            cached[prefab].Push(instance);
            all.Add(instance);

#if UNITY_EDITOR
            if (hideCachedObjectsInHierarchy)
            {
                instance.hideFlags |= HideFlags.HideInHierarchy;
            }
#endif
        }

        // HELPERS

        [Serializable]
        sealed class CacheObject
        {
            public int count;
            public GameObject gameObject;
        }

        sealed class DeferredReturn
        {
            public GameObject GameObject;
            public float Delay;

            public void Reset()
            {
                GameObject = null;
                Delay = 0.0f;
            }
        }
    }
}
