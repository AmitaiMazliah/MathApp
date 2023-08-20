using MathApp.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(UIToggle), true)]
public class UIToggleEditor : ToggleEditor
{
    // PRIVATE METHODS

    private SerializedProperty playValueChangedSound;
    private SerializedProperty customValueChangedSound;

    // ButtonEditor INTERFACE

    protected override void OnEnable()
    {
        base.OnEnable();

        playValueChangedSound = serializedObject.FindProperty("playValueChangedSound");
        customValueChangedSound = serializedObject.FindProperty("customValueChangedSound");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(playValueChangedSound);

        if (playValueChangedSound.boolValue == true)
        {
            EditorGUILayout.PropertyField(customValueChangedSound);
        }

        serializedObject.ApplyModifiedProperties();
    }
}