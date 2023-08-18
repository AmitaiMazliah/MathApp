using System.Collections.Generic;
using UnityEngine;
using UnityScene = UnityEngine.SceneManagement.Scene;

namespace MathApp
{
    public static class SceneExtensions
    {
        public static T GetComponent<T>(this UnityScene scene, bool includeInactive = false) where T : class
        {
            List<GameObject> roots = ListPool<GameObject>.Shared.Get(16);
            scene.GetRootGameObjects(roots);

            T component = default;

            for (int i = 0, count = roots.Count; i < count; ++i)
            {
                component = roots[i].GetComponentInChildren<T>(includeInactive);
                if (component != null)
                    break;
            }

            ListPool<GameObject>.Shared.Return(roots);
            return component;
        }

        public static List<T> GetComponents<T>(this UnityScene scene, bool includeInactive = false) where T : class
        {
            List<T>          allComponents    = new List<T>();
            List<T>          objectComponents = ListPool<T>.Shared.Get(16);
            List<GameObject> sceneRootObjects = ListPool<GameObject>.Shared.Get(16);

            scene.GetRootGameObjects(sceneRootObjects);

            for (int i = 0, count = sceneRootObjects.Count; i < count; ++i)
            {
                sceneRootObjects[i].GetComponentsInChildren(includeInactive, objectComponents);
                allComponents.AddRange(objectComponents);
                objectComponents.Clear();
            }

            ListPool<GameObject>.Shared.Return(sceneRootObjects);
            ListPool<T>.Shared.Return(objectComponents);

            return allComponents;
        }

        public static void GetComponents<T>(this UnityScene scene, List<T> components, bool includeInactive = false) where T : class
        {
            List<T>          objectComponents = ListPool<T>.Shared.Get(16);
            List<GameObject> sceneRootObjects = ListPool<GameObject>.Shared.Get(16);

            scene.GetRootGameObjects(sceneRootObjects);
            components.Clear();

            for (int i = 0, count = sceneRootObjects.Count; i < count; ++i)
            {
                sceneRootObjects[i].GetComponentsInChildren(includeInactive, objectComponents);
                components.AddRange(objectComponents);
                objectComponents.Clear();
            }

            ListPool<GameObject>.Shared.Return(sceneRootObjects);
            ListPool<T>.Shared.Return(objectComponents);
        }
    }
}
