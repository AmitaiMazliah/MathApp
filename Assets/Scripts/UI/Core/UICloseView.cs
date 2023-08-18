using UnityEngine;

namespace MathApp.UI
{
	public class UICloseView : UIView
	{
		public UIView BackView { get; set; }

		public UIButton CloseButton => closeButton;

		// PRIVATE MEMBERS

		[SerializeField]
		private UIButton closeButton;

		// PUBLIC METHODS

		public void CloseWithBack()
		{
			OnCloseButton();
		}

		// UIVIEW INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			if (closeButton != null)
			{
				closeButton.onClick.AddListener(OnCloseButton);
			}
		}

		protected override void OnDeinitialize()
		{
			if (closeButton != null)
			{
				closeButton.onClick.RemoveListener(OnCloseButton);
			}

			base.OnDeinitialize();
		}

		protected override bool OnBackAction()
		{
			if (IsInteractable == false)
				return false;

			OnCloseButton();

			if (closeButton != null)
			{
				closeButton.PlayActionSound(UIAction.Click);
			}

			return true;
		}

		protected virtual void OnCloseButton()
		{
			Close();

			if (BackView != null)
			{
				Open(BackView);
				BackView = null;
			}
		}
	}
}
