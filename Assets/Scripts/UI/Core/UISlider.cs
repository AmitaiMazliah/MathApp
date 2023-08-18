using MathApp.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace MathApp.UI
{
    public class UISlider : Slider
    {
        // PRIVATE MEMBERS

        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private string valueFormat = "f1";
        [SerializeField] private bool playValueChangedSound = true;
        [SerializeField] private AudioCueSO customValueChangedSound;

        private UIWidget parent;

        // PUBLIC METHODS

        public void SetValue(float value)
        {
            SetValueWithoutNotify(value);
            UpdateValueText();
        }

        // MONOBEHAVIOR

        protected override void Awake()
        {
            base.Awake();

            onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDestroy()
        {
            onValueChanged.RemoveListener(OnValueChanged);

            base.OnDestroy();
        }

        // Slider INTERFACE

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (IsActive() && IsInteractable() && eventData.button == PointerEventData.InputButton.Left)
            {
                PlayValueChangedSound();
            }

            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                PlayValueChangedSound();
            }

            base.OnPointerUp(eventData);
        }

        // PRIVATE METHODS

        private void OnValueChanged(float value)
        {
            UpdateValueText();
        }

        private void UpdateValueText()
        {
            if (valueText == null)
                return;

            valueText.text = value.ToString(valueFormat);
        }

        private void PlayValueChangedSound()
        {
            if (playValueChangedSound == false)
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
