using MathApp.Events;
using UnityEngine;
using TMPro;

namespace MathApp.UI
{
	public class UIMainMenuView : UITabsView
    {
        [SerializeField] UIButton quitButton;

        [SerializeField] VoidEventChannelSO onExitGame;

		protected override void OnInitialize()
		{
			base.OnInitialize();

			// creditsButton.onClick.AddListener(OnCreditsButton);
			// changeNicknameButton.onClick.AddListener(OnChangeNicknameButton);
			quitButton.onClick.AddListener(OnQuitButton);
		}

        protected override void OnDeinitialize()
		{
			// creditsButton.onClick.RemoveListener(OnCreditsButton);
			// changeNicknameButton.onClick.RemoveListener(OnChangeNicknameButton);
			quitButton.onClick.RemoveListener(OnQuitButton);

			base.OnDeinitialize();
		}

		protected override void OnOpen()
		{
			base.OnOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}

		protected override bool OnBackAction()
		{
			if (IsInteractable == false)
				return false;

			OnQuitButton();
			return true;
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
	}
}
