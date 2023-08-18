using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class IntRangeAttribute : PropertyAttribute
    {
        public IntRangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
        public int Min { get; private set; }
        public int Max { get; private set; }
    }
}
