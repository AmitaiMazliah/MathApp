using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class DelayDestroyableObject
    {
        public GameObject gameObject;
        [Tooltip("After how many seconds should it be destroyed?")]
        public float delay;
    }
}
