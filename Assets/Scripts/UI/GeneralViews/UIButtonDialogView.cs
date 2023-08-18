using TMPro;

namespace MathApp.UI
{
	public class UIButtonDialogView : UIDialogView
	{
		// PUBLIC MEMBERS

		public UIButton        confirmButton;
		public TextMeshProUGUI confirmButtonText;

		// PRIVATE MEMBERS

		private string defaultOkButtonText;

		// PUBLIC METHODS

		public override void Clear()
		{
			base.Clear();

			confirmButtonText.SetTextSafe(defaultOkButtonText);
		}

		// UIView INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			confirmButton.onClick.AddListener(OnConfirmButton);

			defaultOkButtonText = confirmButtonText.GetTextSafe();
		}

		protected override void OnDeinitialize()
		{
			confirmButton.onClick.RemoveListener(OnConfirmButton);

			base.OnDeinitialize();
		}

		// PRIVATE METHODS

		private void OnConfirmButton()
		{
			Close();
		}
	}
}
