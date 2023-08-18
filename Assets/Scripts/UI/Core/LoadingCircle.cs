using UnityEngine;

namespace Tempname.UI
{
    public class LoadingCircle : MonoBehaviour
    {
        [SerializeField] float rotateSpeed = 200f;
        RectTransform rectComponent;

        void Awake()
        {
            rectComponent = GetComponent<RectTransform>();
        }

        void Update()
        {
            rectComponent.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        }
    }
}
