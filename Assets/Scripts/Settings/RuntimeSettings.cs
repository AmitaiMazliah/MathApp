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

    public void Initialize()
    {
        Options.Initialize(defaultOptions, enablePersistency, "Options.V3.");

        // Resolution = GetCurrentResolutionIndex();

        changeMusicVolumeEventChannel.RaiseEvent(Options.GetFloat(OptionType.MusicVolume));
        changeSfxVolumeEventChannel.RaiseEvent(Options.GetFloat(OptionType.SfxVolume));
        changeMasterVolumeEventChannel.RaiseEvent(Options.GetFloat(OptionType.MasterVolume));
        
        var localeName = Options.GetString(OptionType.Language);
        var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.LocaleName.StartsWith(localeName));
        LocalizationSettings.SelectedLocale = locale;

        Options.SaveChanges();
    }

    private int GetCurrentResolutionIndex()
    {
        var resolutions = Screen.resolutions;
        if (resolutions == null || resolutions.Length == 0)
            return -1;

        var currentWidth = Mathf.RoundToInt(Screen.width);
        var currentHeight = Mathf.RoundToInt(Screen.height);
        var defaultRefreshRate = resolutions[^1].refreshRateRatio;

        for (var i = 0; i < resolutions.Length; i++)
        {
            var resolution = resolutions[i];

            if (resolution.width == currentWidth && resolution.height == currentHeight &&
                resolution.refreshRateRatio.Equals(defaultRefreshRate))
                return i;
        }

        return -1;
    }
}