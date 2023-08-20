using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "RuntimeSettings", menuName = "Settings/Runtime Settings")]
public class RuntimeSettings : SerializedScriptableObject
{
    public Options Options { get; } = new Options();

    public Dictionary<OptionType, string> defaultOptions = new();

    [SerializeField] private bool enablePersistency;
    [SerializeField] private FloatEventChannelSO changeMasterVolumeEventChannel;
    [SerializeField] private FloatEventChannelSO changeSfxVolumeEventChannel;
    [SerializeField] private FloatEventChannelSO changeMusicVolumeEventChannel;

    void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
    public void Initialize()
    {
        Options.Initialize(defaultOptions, enablePersistency, "Options.V3.");

        // Resolution = GetCurrentResolutionIndex();

        // QualitySettings.vSyncCount = VSync ? 1 : 0;
        // Application.targetFrameRate = LimitFPS ? TargetFPS : -1;

        // changeMusicVolumeEventChannel.RaiseEvent(MusicVolume);
        // changeSfxVolumeEventChannel.RaiseEvent(EffectsVolume);
        // changeMasterVolumeEventChannel.RaiseEvent(MasterVolume);

        Options.SaveChanges();
    }

    private int GetCurrentResolutionIndex()
    {
        var resolutions = Screen.resolutions;
        if (resolutions == null || resolutions.Length == 0)
            return -1;

        int currentWidth = Mathf.RoundToInt(Screen.width);
        int currentHeight = Mathf.RoundToInt(Screen.height);
        int defaultRefreshRate = resolutions[^1].refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            var resolution = resolutions[i];

            if (resolution.width == currentWidth && resolution.height == currentHeight &&
                resolution.refreshRate == defaultRefreshRate)
                return i;
        }

        return -1;
    }
}