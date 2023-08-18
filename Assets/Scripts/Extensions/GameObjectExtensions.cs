using System.Collections.Generic;
using UnityEngine;

namespace MathApp
{
    public static partial class GameObjectExtensions
    {
        // PUBLIC METHODS

        public static T GetComponentNoAlloc<T>(this GameObject gameObject) where T : class
        {
            return GameObjectExtensions<T>.GetComponentNoAlloc(gameObject);
        }

        public static void SetActiveSafe(this GameObject gameObject, bool value)
        {
            if (gameObject == null)
                return;

            if (gameObject.activeSelf == value)
                return;

            gameObject.SetActive(value);
        }
    }

    public static partial class GameObjectExtensions<T> where T : class
    {
        // PRIVATE MEMBERS

        private static List<T> components = new List<T>();

        // PUBLIC METHODS

        public static T GetComponentNoAlloc(GameObject gameObject)
        {
            components.Clear();

            gameObject.GetComponents(components);

            if (components.Count > 0)
            {
                T component = components[0];

                components.Clear();

                return component;
            }

            return null;
        }
    }
}
