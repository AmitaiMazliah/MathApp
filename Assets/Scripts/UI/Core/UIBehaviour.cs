using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
	public class UIBehaviour : MonoBehaviour
	{
        CanvasGroup cachedCanvasGroup;
        bool canvasGroupChecked;

        RectTransform cachedRectTransform;
        bool rectTransformChecked;

        Image cachedImage;
        bool imageChecked;

        Animation cachedAnimation;
        bool animationChecked;

        Animator cachedAnimator;
        bool animatorChecked;

        TextMeshProUGUI cachedText;
        bool textChecked;

		public CanvasGroup CanvasGroup
		{
			get
			{
				if (canvasGroupChecked == false)
				{
					cachedCanvasGroup = GetComponent<CanvasGroup>();
					canvasGroupChecked = true;
				}

				return cachedCanvasGroup;
			}
		}

		public RectTransform RectTransform
		{
			get
			{
				if (rectTransformChecked == false)
				{
					cachedRectTransform = transform as RectTransform;
					rectTransformChecked = true;
				}

				return cachedRectTransform;
			}
		}

		public Image Image
		{
			get
			{
				if (imageChecked == false)
				{
					cachedImage = GetComponent<Image>();
					imageChecked = true;
				}

				return cachedImage;
			}
		}

		public Animation Animation
		{
			get
			{
				if (animationChecked == false)
				{
					cachedAnimation = GetComponent<Animation>();
					animationChecked = true;
				}

				return cachedAnimation;
			}
		}

		public Animator Animator
		{
			get
			{
				if (animatorChecked == false)
				{
					cachedAnimator = GetComponent<Animator>();
					animatorChecked = true;
				}

				return cachedAnimator;
			}
		}

		public TextMeshProUGUI Text
		{
			get
			{
				if (textChecked == false)
				{
					cachedText = GetComponent<TextMeshProUGUI>();
					textChecked = true;
				}

				return cachedText;
			}
		}
	}
}
