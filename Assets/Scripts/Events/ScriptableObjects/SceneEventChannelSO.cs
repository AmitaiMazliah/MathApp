using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used for Events that have a string argument.
/// </summary>
[CreateAssetMenu(menuName = "Events/Scene Event Channel")]
public class SceneEventChannelSO : DescriptionBaseSO
{
    [SerializeField] bool logEvents;

    public event UnityAction<Scene> OnEventRaised;

    public void RaiseEvent(Scene value)
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
    void TestEvent(Scene value)
    {
        RaiseEvent(value);
    }
}
