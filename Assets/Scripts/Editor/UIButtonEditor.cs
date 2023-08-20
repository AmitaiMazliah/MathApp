using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(UIButton), true)]
public class UIButtonEditor : ButtonEditor
{
    private SerializedProperty playClickSound;
    private SerializedProperty customClickSound;

    protected override void OnEnable()
    {
        base.OnEnable();

        playClickSound = serializedObject.FindProperty("playClickSound");
        customClickSound = serializedObject.FindProperty("customSoundEffects");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(playClickSound);

        if (playClickSound.boolValue)
        {
            EditorGUILayout.PropertyField(customClickSound);
        }

        serializedObject.ApplyModifiedProperties();
    }
}