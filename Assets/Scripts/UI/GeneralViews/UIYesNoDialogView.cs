using UnityEngine;
using System;
using TMPro;

namespace MathApp.UI
{
	public class UIYesNoDialogView : UIDialogView
	{
		// PUBLIC MEMBERS

		public bool                   Result        { get; private set; }
		public new event Action<bool> HasClosed;

		public UIButton               yesButton;
		public TextMeshProUGUI        yesButtonText;
		public UIButton               noButton;
		public TextMeshProUGUI        noButtonText;

		// PRIVATE MEMBERS

		[SerializeField]
		private string defaultYesButtonText = "Confirm";
		[SerializeField]
		private string defaultNoButtonText  = "Cancel";

		// PUBLIC METHODS

		public override void Clear()
		{
			base.Clear();

			yesButtonText.SetTextSafe(defaultYesButtonText);
			noButtonText.SetTextSafe(defaultNoButtonText);
		}

		// UIView INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			yesButton.onClick.AddListener(OnYesButton);
			noButton.onClick.AddListener(OnNoButton);
		}

		protected override void OnDeinitialize()
		{
			yesButton.onClick.RemoveListener(OnYesButton);
			noButton.onClick.RemoveListener(OnNoButton);

			base.OnDeinitialize();
		}

		protected override void OnOpen()
		{
			base.OnOpen();

			Result = false;
		}

		protected override void OnClose()
		{
			base.OnClose();

			if (HasClosed != null)
			{
				HasClosed.Invoke(Result);
				HasClosed = null;
			}
		}

		// PRIVATE METHODS

		private void OnYesButton()
		{
			Result = true;
			Close();
		}

		private void OnNoButton()
		{
			Result = false;
			Close();
		}
	}
}
