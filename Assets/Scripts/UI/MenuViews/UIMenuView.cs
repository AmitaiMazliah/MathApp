using MathApp.Events;
using MathApp.SceneManagement;
using MathApp.UI;
using UnityEngine;

public class UIMainMenuView : UIView
{
    [SerializeField] private UIButton quitButton;
    [SerializeField] private UIButton settingButton;

    [SerializeField] VoidEventChannelSO onExitGame;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        quitButton.onClick.AddListener(OnQuitButton);
        settingButton.onClick.AddListener(OnSettingButton);
    }

    protected override void OnDeinitialize()
    {
        quitButton.onClick.RemoveListener(OnQuitButton);
        settingButton.onClick.RemoveListener(OnSettingButton);

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
        
    }
}