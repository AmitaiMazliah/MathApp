using UnityEngine;
using MathApp.UI;

namespace MathApp
{
	public static class UIExtensions
	{
		public static void SetActive(this UnityEngine.EventSystems.UIBehaviour @this, bool value)
		{
			if (@this == null)
				return;

			if (@this.gameObject.activeSelf == value)
				return;

			@this.gameObject.SetActive(value);
		}

		//public static void SetActive(this UIWidget @this, bool value)
		//{
		//	if (@this == null)
		//		return;

		//	if (@this.gameObject.activeSelf == value)
		//		return;

		//	@this.gameObject.SetActive(value);
		//}

		public static void SetActive(this UIBehaviour @this, bool value)
		{
			if (@this == null)
				return;

			if (@this.gameObject.activeSelf == value)
				return;

			@this.gameObject.SetActive(value);
		}

		public static void SetVisibility(this CanvasGroup @this, bool value)
		{
			if (@this == null)
				return;

			@this.alpha = value ? 1f : 0f;
			@this.interactable = value;
			@this.blocksRaycasts = value;
		}

		public static void SetTextSafe(this TMPro.TextMeshProUGUI @this, string text)
		{
			if (@this == null)
				return;

			@this.text = text;
		}

		public static string GetTextSafe(this TMPro.TextMeshProUGUI @this)
		{
			if (@this == null)
				return null;

			return @this.text;
		}
	}
}
