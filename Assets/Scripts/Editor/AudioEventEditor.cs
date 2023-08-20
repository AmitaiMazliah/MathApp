using MathApp.Audio;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioCueSO), true)]
public class AudioEventEditor : Editor
{
    [SerializeField] private AudioSource previewer;

    public void OnEnable()
    {
        previewer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
        DestroyImmediate(previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            PlayAudio((AudioCueSO)target);
        }

        EditorGUI.EndDisabledGroup();
    }

    private void PlayAudio(AudioCueSO audioCue)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips();
        int nOfClips = clipsToPlay.Length;

        for (int i = 0; i < nOfClips; i++)
        {
            previewer.clip = clipsToPlay[i];
            previewer.loop = audioCue.looping;
            previewer.Play();
        }
    }
}