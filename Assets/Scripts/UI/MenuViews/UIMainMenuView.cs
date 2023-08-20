using MathApp.Events;
using MathApp.SceneManagement;
using UnityEngine;

namespace MathApp.UI
{
	public class UIMainMenuView : UITabsView
    {
        [SerializeField] private UIButton multiplicationTableButton;

        [SerializeField] private GameSceneSO multiplicationTableScene;

        [SerializeField] LoadEventChannelSO loadSceneEvent;

		protected override void OnInitialize()
		{
			base.OnInitialize();

			multiplicationTableButton.onClick.AddListener(ChangeScene);
		}

        protected override void OnDeinitialize()
		{
			multiplicationTableButton.onClick.RemoveListener(ChangeScene);

			base.OnDeinitialize();
		}

		protected override bool OnBackAction()
		{
			if (IsInteractable == false)
				return false;

			return true;
		}

        private void ChangeScene()
        {
	        loadSceneEvent.RaiseEvent(multiplicationTableScene, true);
        }
	}
}
