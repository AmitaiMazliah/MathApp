﻿using UnityEngine;

namespace Tempname.Utils
{
    public class Singleton<Instance> : MonoBehaviour where Instance : Singleton<Instance>
    {
        public static Instance instance;
        public bool isPersistant;

        protected virtual void Awake()
        {
            if (isPersistant)
            {
                if (!instance)
                {
                    instance = this as Instance;
                }
                else
                {
                    Destroy(gameObject);
                }
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                instance = this as Instance;
            }
        }
    }
}