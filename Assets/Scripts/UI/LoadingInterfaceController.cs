using UnityEngine;

namespace MathApp.UI
{
    public class LoadingInterfaceController : MonoBehaviour
    {
        [SerializeField] GameObject loadingInterface;
        [SerializeField] Camera cam;

        [Header("Listening on")]
        [SerializeField] BoolEventChannelSO toggleLoadingScreen;

        void OnEnable()
        {
            toggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
        }

        void OnDisable()
        {
            toggleLoadingScreen.OnEventRaised -= ToggleLoadingScreen;
        }

        void ToggleLoadingScreen(bool state)
        {
            loadingInterface.SetActive(state);
            cam.SetActive(state);
        }
    }
}
