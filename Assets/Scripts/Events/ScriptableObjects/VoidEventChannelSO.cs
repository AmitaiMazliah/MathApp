using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MathApp.Events
{
    /// <summary>
    /// This class is used for Events that have no arguments (Example: Exit game event)
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO : DescriptionBaseSO
    {
        [SerializeField] bool logEvents;

        public event UnityAction OnEventRaised;

        public void RaiseEvent()
        {
#if UNITY_EDITOR
            if (logEvents)
            {
                var method = new StackFrame(1).GetMethod();
                var callingClassName = method.DeclaringType?.Name;
                var callingFuncName = method.Name;
                UnityEngine.Debug.Log($"Event {name} has been raised by {callingClassName}.{callingFuncName}");
            }
#endif
            OnEventRaised?.Invoke();
        }

        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        private void TestEvent()
        {
            RaiseEvent();
        }
    }
}
