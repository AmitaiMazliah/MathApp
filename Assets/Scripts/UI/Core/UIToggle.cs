using MathApp.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UIToggle : Toggle
    {
        // PRIVATE MEMBERS

        [SerializeField] private bool playValueChangedSound = true;

        [SerializeField] private AudioCueSO customValueChangedSound;

        private UIWidget parent;

        // MONOBEHAVIOR

        protected override void Awake()
        {
            base.Awake();

            // Toggle Awake is executed in Editor as well
            if (Application.isPlaying)
            {
                onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected override void OnDestroy()
        {
            onValueChanged.RemoveListener(OnValueChanged);

            base.OnDestroy();
        }

        // PRIVATE METHODS

        private void OnValueChanged(bool isSelected)
        {
            if (playValueChangedSound == false)
                return;

            if (isSelected == false && group != null && group.allowSwitchOff == false)
                return;

            if (parent == null)
            {
                parent = GetComponentInParent<UIWidget>();
            }

            if (parent == null)
                return;

            if (customValueChangedSound != null)
            {
                parent.PlaySound(customValueChangedSound);
            }
            else
            {
                parent.PlayActionSound(UIAction.Click);
            }
        }
    }
}
