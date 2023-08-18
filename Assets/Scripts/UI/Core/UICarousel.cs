using System.Collections.Generic;
using MathApp.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UICarousel : TMP_Dropdown
    {
        [SerializeField, ColorUsage(true, true)]
        Color selectedIndexColor;

        [SerializeField, ColorUsage(true, true)]
        Color defaultIndexColor;

        [SerializeField] Transform indexIndicatorsParent;
        [SerializeField] Image indexIndicatorPrefab;
        [SerializeField] UIButton nextButton;
        [SerializeField] UIButton previousButton;
        [SerializeField] private bool playValueChangedSound = true;

        [SerializeField] private AudioCueSO customValueChangedSound;

        private UIWidget parent;
        Image[] indexIndicators;

        protected override void Awake()
        {
            base.Awake();

            // Toggle Awake is executed in Editor as well
            if (Application.isPlaying)
            {
                onValueChanged.AddListener(OnValueChanged);
                nextButton.onClick.AddListener(NextValue);
                previousButton.onClick.AddListener(PreviousValue);

                BuildIndicators(options);
            }
        }

        protected override void OnDestroy()
        {
            onValueChanged.RemoveListener(OnValueChanged);
            nextButton.onClick.RemoveListener(NextValue);
            previousButton.onClick.RemoveListener(PreviousValue);

            base.OnDestroy();
        }

        void NextValue()
        {
            var newValue = value + 1;
            indexIndicators[value].color = defaultIndexColor;
            value = newValue >= options.Count ? 0 : newValue;
            // SetValueWithoutNotify(newValue >= options.Count ? 0 : newValue);
            indexIndicators[value].color = selectedIndexColor;
        }

        void PreviousValue()
        {
            var newValue = value - 1;
            indexIndicators[value].color = defaultIndexColor;
            value = newValue < 0 ? options.Count - 1 : newValue;
            SetValueWithoutNotify(newValue < 0 ? options.Count - 1 : newValue);
            indexIndicators[value].color = selectedIndexColor;
        }

        public new void SetValueWithoutNotify(int input)
        {
            indexIndicators[value].color = defaultIndexColor;
            base.SetValueWithoutNotify(input);
            indexIndicators[value].color = selectedIndexColor;
        }

        public new void ClearOptions()
        {
            for (var i = 0; i < indexIndicators.Length; i++)
            {
                DestroyImmediate(indexIndicators[i].gameObject);
            }

            indexIndicators = null;

            base.ClearOptions();
        }

        public new void AddOptions(List<OptionData> options)
        {
            BuildIndicators(options);
            base.AddOptions(options);
        }

        private void BuildIndicators(List<OptionData> options)
        {
            indexIndicators = new Image[options.Count];
            for (var i = 0; i < options.Count; i++)
            {
                indexIndicators[i] = Instantiate(indexIndicatorPrefab, indexIndicatorsParent);
                if (i == value)
                {
                    indexIndicators[i].color = selectedIndexColor;
                }
                else
                {
                    indexIndicators[i].color = defaultIndexColor;
                }
            }
        }

        private void OnValueChanged(int selectedValueIndex)
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
