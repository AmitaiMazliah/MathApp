using MathApp;
using MathApp.Events;
using MathApp.UI;
using UnityEngine;
using UnityEngine.UI;

public class UiMenuView : UIView
{
    [SerializeField] private UIButton quitButton;
    [SerializeField] private UIButton settingButton;
    [SerializeField] private UIButton muteButton;
    [SerializeField] private Image xIcon;

    [SerializeField] VoidEventChannelSO onExitGame;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        quitButton.onClick.AddListener(OnQuitButton);
        settingButton.onClick.AddListener(OnSettingButton);
        muteButton.onClick.AddListener(ToggleMute);
    }

    protected override void OnDeinitialize()
    {
        quitButton.onClick.RemoveListener(OnQuitButton);
        settingButton.onClick.RemoveListener(OnSettingButton);
        muteButton.onClick.RemoveListener(ToggleMute);

        base.OnDeinitialize();
    }

    public void OnQuitButton()
    {
        var dialog = Open<UIYesNoDialogView>();

        dialog.title.text = "EXIT GAME";
        dialog.description.text = "Are you sure you want to exit the game?";

        dialog.yesButtonText.text = "EXIT";
        dialog.noButtonText.text = "CANCEL";

        dialog.HasClosed += (result) =>
        {
            if (result)
            {
                onExitGame.RaiseEvent();
            }
        };
    }

    private void OnSettingButton()
    {
        Toggle<UISettingsView>();
    }
    
    private void ToggleMute()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            xIcon.enabled = true;
        }
        else
        {
            AudioListener.volume = 1;
            xIcon.enabled = false;
        }
    }
}