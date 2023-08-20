using MathApp.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(UISlider), true)]
public class UISliderEditor : SliderEditor
{
    // PRIVATE METHODS

    private SerializedProperty valueText;
    private SerializedProperty valueFormat;
    private SerializedProperty playValueChangedSound;
    // private SerializedProperty customValueChangedSound;

    // ButtonEditor INTERFACE

    protected override void OnEnable()
    {
        base.OnEnable();

        valueText = serializedObject.FindProperty("valueText");
        valueFormat = serializedObject.FindProperty("valueFormat");
        playValueChangedSound = serializedObject.FindProperty("playValueChangedSound");
        // customValueChangedSound = serializedObject.FindProperty("customValueChangedSound");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(valueText);
        EditorGUILayout.PropertyField(valueFormat);

        EditorGUILayout.PropertyField(playValueChangedSound);

        if (playValueChangedSound.boolValue)
        {
            // EditorGUILayout.PropertyField(customValueChangedSound);
        }

        serializedObject.ApplyModifiedProperties();
    }
}