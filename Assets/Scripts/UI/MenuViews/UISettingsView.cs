using System;
using System.Collections.Generic;
using System.Globalization;
using MathApp;
using MathApp.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISettingsView : UIView
{
    [SerializeField] private UISlider masterVolumeSlider;
    [SerializeField] private UISlider musicVolumeSlider;
    [SerializeField] private UISlider effectsVolumeSlider;

    [SerializeField] private UICarousel graphicsQuality;
    [SerializeField] private UICarousel resolution;
    [SerializeField] private UICarousel windowMode;

    [SerializeField] private UIButton confirmButton;
    [SerializeField] private UIButton resetButton;

    [Header("Events")]
    [SerializeField] FloatEventChannelSO sfxVolumeEventChannel;
    [SerializeField] FloatEventChannelSO musicVolumeEventChannel;
    [SerializeField] FloatEventChannelSO masterVolumeEventChannel;

    private List<ResolutionData> validResolutions = new List<ResolutionData>(32);

    protected override void OnInitialize()
    {
        base.OnInitialize();

        confirmButton.onClick.AddListener(OnConfirmButton);
        resetButton.onClick.AddListener(OnResetButton);

        masterVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        //
        // sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        // aimSensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        //
        graphicsQuality.onValueChanged.AddListener(OnGraphicsChanged);
        resolution.onValueChanged.AddListener(OnGraphicsChanged);
        // targetFPS.onValueChanged.AddListener(OnTargetFPSChanged);
        // limitFPS.onValueChanged.AddListener(OnLimitFPSChanged);

        windowMode.onValueChanged.AddListener(OnWindowedChanged);
    }

    protected override void OnDeinitialize()
    {
        confirmButton.onClick.RemoveListener(OnConfirmButton);
        resetButton.onClick.RemoveListener(OnResetButton);

        masterVolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        musicVolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        effectsVolumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);

        // sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        // aimSensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        //
        graphicsQuality.onValueChanged.RemoveListener(OnGraphicsChanged);
        resolution.onValueChanged.RemoveListener(OnGraphicsChanged);
        // targetFPS.onValueChanged.RemoveListener(OnTargetFPSChanged);
        // limitFPS.onValueChanged.RemoveListener(OnLimitFPSChanged);

        windowMode.onValueChanged.RemoveListener(OnWindowedChanged);

        base.OnDeinitialize();
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        PrepareResolutionDropdown();

        LoadValues();
    }

    protected override void OnClose()
    {
        base.OnClose();

        Context.RuntimeSettings.Options.DiscardChanges();

        NotifyVolumeChanged();
    }

    protected override bool OnBackAction()
    {
        Close();
        return true;
    }

    protected override void OnTick()
    {
        base.OnTick();

        // confirmButton.interactable = Context.RuntimeSettings.Options.HasUnsavedChanges;

        // limitFPS.SetActive(vSync.isOn == false);
        // targetFPS.SetActive(vSync.isOn == false && limitFPS.isOn);
    }

    private void LoadValues()
    {
        var runtimeSettings = Context.RuntimeSettings;

        masterVolumeSlider.SetOptionsValueFloat(OptionType.MasterVolume,
            runtimeSettings.Options.GetFloat(OptionType.MasterVolume));
        musicVolumeSlider.SetOptionsValueFloat(OptionType.MusicVolume,
            runtimeSettings.Options.GetFloat(OptionType.MusicVolume));
        effectsVolumeSlider.SetOptionsValueFloat(OptionType.SfxVolume,
            runtimeSettings.Options.GetFloat(OptionType.SfxVolume));

        // sensitivitySlider.SetOptionsValueFloat(runtimeSettings.Options.GetValue(RuntimeSettings.KEY_SENSITIVITY));
        // aimSensitivitySlider.SetOptionsValueFloat(runtimeSettings.Options.GetValue(RuntimeSettings.KEY_AIM_SENSITIVITY));

        windowMode.SetValueWithoutNotify(runtimeSettings.Options.GetBool(OptionType.FullScreen) ? 0 : 1);
        // graphicsQuality.SetValueWithoutNotify(runtimeSettings.GraphicsQuality);
        // resolution.SetValueWithoutNotify(validResolutions.FindIndex(t => t.Index == runtimeSettings.Resolution));
        // targetFPS.SetOptionsValueInt(runtimeSettings.Options.GetValue(RuntimeSettings.KEY_TARGET_FPS));
        // limitFPS.SetIsOnWithoutNotify(runtimeSettings.LimitFPS);
        // vSync.SetValueWithoutNotify(runtimeSettings.VSync ? 0 : 1);
    }

    private void OnConfirmButton()
    {
        Context.RuntimeSettings.Options.SaveChanges();
        
        var runtimeSettings = Context.RuntimeSettings;

        var resolution =
            Screen.resolutions[
                runtimeSettings.Options.GetInt(OptionType.Resolution) < 0 ? Screen.resolutions.Length - 1 :
                    runtimeSettings.Options.GetInt(OptionType.Resolution)];
        Screen.SetResolution(resolution.width, resolution.height, windowMode.value == 0);
        
        // QualitySettings.SetQualityLevel(runtimeSettings.GraphicsQuality);
        //
        // // Application.targetFrameRate = runtimeSettings.LimitFPS == true ? runtimeSettings.TargetFPS : -1;
        // QualitySettings.vSyncCount = runtimeSettings.VSync ? 1 : 0;
    }

    private void OnResetButton()
    {
        // var options = Context.RuntimeSettings.Options;

        // options.ResetAllValuesToDefault();
        // options.ResetValueToDefault(RuntimeSettings.KeyEffectsVolume, false);
        // options.ResetValueToDefault(RuntimeSettings.KeyMusicVolume, false);
        // options.ResetValueToDefault(RuntimeSettings.KeyGraphicsQuality, false);
        // options.ResetValueToDefault(RuntimeSettings.KeyResolution, false);
        // options.ResetValueToDefault(RuntimeSettings.KeyWindowed, false);
        // options.ResetValueToDefault(RuntimeSettings.KEY_LIMIT_FPS, false);
        // options.ResetValueToDefault(RuntimeSettings.KEY_TARGET_FPS, false);
        // options.ResetValueToDefault(RuntimeSettings.KEY_SENSITIVITY, false);
        // options.ResetValueToDefault(RuntimeSettings.KEY_AIM_SENSITIVITY, false);
        // options.ResetValueToDefault(RuntimeSettings.KeyVsync, false);

        LoadValues();

        NotifyVolumeChanged();
    }

    private void OnVolumeChanged(float value)
    {
        Context.RuntimeSettings.Options.Set(OptionType.MasterVolume,
            masterVolumeSlider.value.ToString(CultureInfo.InvariantCulture), false);
        Context.RuntimeSettings.Options.Set(OptionType.MusicVolume,
            musicVolumeSlider.value.ToString(CultureInfo.InvariantCulture), false);
        Context.RuntimeSettings.Options.Set(OptionType.SfxVolume,
            effectsVolumeSlider.value.ToString(CultureInfo.InvariantCulture), false);

        NotifyVolumeChanged();
    }

    void NotifyVolumeChanged()
    {
        sfxVolumeEventChannel.RaiseEvent(effectsVolumeSlider.value);
        musicVolumeEventChannel.RaiseEvent(musicVolumeSlider.value);
        masterVolumeEventChannel.RaiseEvent(masterVolumeSlider.value);
    }

    private void OnSensitivityChanged(float value)
    {
        // Context.RuntimeSettings.Sensitivity = sensitivitySlider.value;
        // Context.RuntimeSettings.AimSensitivity = aimSensitivitySlider.value;
    }

    private void OnLimitFPSChanged(int value)
    {
        OnGraphicsChanged(-1);
    }

    private void OnTargetFPSChanged(float value)
    {
        OnGraphicsChanged(-1);
    }

    private void OnGraphicsChanged(int value)
    {
        // var runtimeSettings = Context.RuntimeSettings;

        // runtimeSettings.GraphicsQuality = graphicsQuality.value;
        // runtimeSettings.Resolution = validResolutions[resolution.value].Index;
        // // runtimeSettings.TargetFPS = Mathf.RoundToInt(targetFPS.value);
        // // runtimeSettings.LimitFPS = limitFPS.isOn;
        // runtimeSettings.VSync = vSync.value == 0;
    }

    private void OnWindowedChanged(int value)
    {
        // Context.RuntimeSettings.Windowed = value == 1;
    }

    private void PrepareResolutionDropdown()
    {
        validResolutions.Clear();
        var resolutions = Screen.resolutions;

        var defaultRefreshRate = resolutions[^1].refreshRate;

        // Add resolutions in reversed order
        for (var i = resolutions.Length - 1; i >= 0; i--)
        {
            var resolution = resolutions[i];
            if (resolution.refreshRate != defaultRefreshRate)
                continue;

            validResolutions.Add(new ResolutionData(i, resolution));
        }


        var options = ListPool.Get<TMP_Dropdown.OptionData>(16);

        for (var i = 0; i < validResolutions.Count; i++)
        {
            var resolution = validResolutions[i].Resolution;
            options.Add(new TMP_Dropdown.OptionData($"{resolution.width} x {resolution.height}"));
        }

        resolution.ClearOptions();
        resolution.AddOptions(options);

        ListPool.Return(options);
    }

    // HELPERS

    private struct ResolutionData
    {
        public int Index;
        public Resolution Resolution;

        public ResolutionData(int index, Resolution resolution)
        {
            Index = index;
            Resolution = resolution;
        }
    }
}

public static class UISliderExtensions
{
    public static void SetOptionsValueFloat(this UISlider slider, OptionType type, float value)
    {
        var onValueChanged = slider.onValueChanged;
        slider.onValueChanged = new Slider.SliderEvent();

        slider.onValueChanged = onValueChanged;

        slider.SetValue(value);
    }

    public static void SetOptionsValueInt(this UISlider slider, OptionType type, int value)
    {
        var onValueChanged = slider.onValueChanged;
        slider.onValueChanged = new Slider.SliderEvent();

        slider.onValueChanged = onValueChanged;

        slider.SetValue(value);
    }
}