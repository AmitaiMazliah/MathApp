using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class DelayDestroyableCompoent
    {
        public Component component;
        [Tooltip("After how many seconds should it be destroyed?")]
        public float delay;
    }
}
