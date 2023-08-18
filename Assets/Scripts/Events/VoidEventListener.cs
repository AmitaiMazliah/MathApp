using UnityEngine;
using UnityEngine.Events;

namespace MathApp.Events
{
    /// <summary>
    /// A flexible handler for void events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
    /// </summary>
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO channel = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (channel != null) channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (channel != null) channel.OnEventRaised -= Respond;
        }

        private void Respond()
        {
            OnEventRaised?.Invoke();
        }
    }
}
