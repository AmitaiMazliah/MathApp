using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// This class is used for Events that have a int argument.
/// </summary>
[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : DescriptionBaseSO
{
    [SerializeField] bool logEvents;

    public event UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
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
    private void TestEvent(int value)
    {
        RaiseEvent(value);
    }
}
