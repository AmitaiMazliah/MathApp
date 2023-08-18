using UnityEngine;

namespace Tempname.Utils
{
    public class ObjectsChangeLocation : MonoBehaviour
    {
        public Transform[] objects;
        public Vector3 changeInUnits;

        public void ModifyObjectsHeight()
        {
            foreach (var transform in objects)
            {
                transform.position = transform.position + changeInUnits;
            }
        }
    }
}
