using MathApp.UI;
using TMPro.EditorUtilities;
using UnityEditor;

[CustomEditor(typeof(UICarousel), true)]
public class UICarouselEditor : DropdownEditor
{
    SerializedProperty selectedIndexColor;
    SerializedProperty defaultIndexColor;
    SerializedProperty indexIndicatorsParent;
    SerializedProperty indexIndicatorPrefab;
    SerializedProperty nextButton;
    SerializedProperty previousButton;
    private SerializedProperty playValueChangedSound;
    // private SerializedProperty customValueChangedSound;

    // ButtonEditor INTERFACE

    protected override void OnEnable()
    {
        base.OnEnable();

        selectedIndexColor = serializedObject.FindProperty("selectedIndexColor");
        defaultIndexColor = serializedObject.FindProperty("defaultIndexColor");
        indexIndicatorsParent = serializedObject.FindProperty("indexIndicatorsParent");
        indexIndicatorPrefab = serializedObject.FindProperty("indexIndicatorPrefab");
        nextButton = serializedObject.FindProperty("nextButton");
        previousButton = serializedObject.FindProperty("previousButton");
        playValueChangedSound = serializedObject.FindProperty("playValueChangedSound");
        // customValueChangedSound = serializedObject.FindProperty("customValueChangedSound");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(selectedIndexColor);
        EditorGUILayout.PropertyField(defaultIndexColor);
        EditorGUILayout.PropertyField(indexIndicatorsParent);
        EditorGUILayout.PropertyField(indexIndicatorPrefab);
        EditorGUILayout.PropertyField(nextButton);
        EditorGUILayout.PropertyField(previousButton);

        EditorGUILayout.PropertyField(playValueChangedSound);

        if (playValueChangedSound.boolValue)
        {
            // EditorGUILayout.PropertyField(customValueChangedSound);
        }

        serializedObject.ApplyModifiedProperties();
    }
}