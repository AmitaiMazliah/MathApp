using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class TimeUtils : MonoBehaviour
    {
        [SerializeField] private float slowMotion = 0.1f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ToggleSlowMotion();
            }
        }

        private void ToggleSlowMotion()
        {
            Time.timeScale = Time.timeScale == 1f ? slowMotion : 1f;
        }
    }
}
