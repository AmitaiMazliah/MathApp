using UnityEngine;

namespace MathApp.Utils
{
    public static class LayerExtension
    {
        public static int LayerMaskToInt(this LayerMask layer)
        {
            int layerNumber = 0;
            int layerValue = layer.value;
            while (layerValue > 0)
            {
                layerValue >>= 1;
                layerNumber++;
            }
            return layerNumber - 1;
        }
    }
}
