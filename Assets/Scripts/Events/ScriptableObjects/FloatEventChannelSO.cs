using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// This class is used for Events that have a float argument.
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : DescriptionBaseSO
{
    [SerializeField] bool logEvents;

    public event UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
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
        OnEventRaised?.Invoke(value);
    }

    [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
    private void TestEvent(float value)
    {
        RaiseEvent(value);
    }
}
