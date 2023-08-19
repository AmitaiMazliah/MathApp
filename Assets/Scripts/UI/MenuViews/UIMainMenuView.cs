using MathApp.Events;
using MathApp.SceneManagement;
using UnityEngine;

namespace MathApp.UI
{
	public class UIMainMenuView : UITabsView
    {
        [SerializeField] UIButton quitButton;
        [SerializeField] private UIButton multiplicationTableButton;

        [SerializeField] private GameSceneSO multiplicationTableScene;

        [SerializeField] VoidEventChannelSO onExitGame;
        [SerializeField] LoadEventChannelSO loadSceneEvent;

		protected override void OnInitialize()
		{
			base.OnInitialize();

			quitButton.onClick.AddListener(OnQuitButton);
			multiplicationTableButton.onClick.AddListener(ChangeScene);
		}

        protected override void OnDeinitialize()
		{
			quitButton.onClick.RemoveListener(OnQuitButton);
			multiplicationTableButton.onClick.RemoveListener(ChangeScene);

			base.OnDeinitialize();
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

        private void ChangeScene()
        {
	        loadSceneEvent.RaiseEvent(multiplicationTableScene, true);
        }
	}
}
