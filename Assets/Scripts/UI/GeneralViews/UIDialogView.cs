using TMPro;

namespace MathApp.UI
{
	public class UIDialogView : UICloseView
	{
		// PUBLIC MEMBERS

		public TextMeshProUGUI title;
		public TextMeshProUGUI description;

		// PRIVATE MEMBERS

		private string defaultTitleText;
		private string defaultDescriptionText;

		// PUBLIC METHODS

		public virtual void Clear()
		{
			title.SetTextSafe(defaultTitleText);
			description.SetTextSafe(defaultDescriptionText);
		}

		// UIView INTERFACE

		protected override void OnInitialize()
		{
			base.OnInitialize();

			defaultTitleText = title.GetTextSafe();
			defaultDescriptionText = description.GetTextSafe();
		}

		protected override void OnClose()
		{
			base.OnClose();

			Clear();
		}
	}
}
