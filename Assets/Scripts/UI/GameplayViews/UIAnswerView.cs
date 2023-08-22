using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UIAnswerView : UIBehaviour
    {
        [SerializeField] private TMP_InputField wholeNumberInput;
        [SerializeField] private TMP_InputField numeratorInput;
        [SerializeField] private TMP_InputField denominatorInput;
        
        public event UnityAction<decimal> OnAnswerSet;

        public decimal? Answer { get; private set; }

        private Image[] inputImages;

        private void Awake()
        {
            inputImages = new[]
            {
                wholeNumberInput.GetComponent<Image>(),
                numeratorInput.GetComponent<Image>(),
                denominatorInput.GetComponent<Image>(),
            };
        }

        private void OnEnable()
        {
            wholeNumberInput.onEndEdit.AddListener(SetAnswer);
            numeratorInput.onEndEdit.AddListener(SetAnswer);
            denominatorInput.onEndEdit.AddListener(SetAnswer);
        }

        private void OnDisable()
        {
            wholeNumberInput.onEndEdit.RemoveListener(SetAnswer);
            numeratorInput.onEndEdit.RemoveListener(SetAnswer);
            denominatorInput.onEndEdit.RemoveListener(SetAnswer);
        }

        public void SetCorrect(bool correct)
        {
            foreach (var inputImage in inputImages)
            {
                if (correct) inputImage.color = Color.green;
                else inputImage.color = Color.red;
            }
        }

        public void Clear()
        {
            wholeNumberInput.text = string.Empty;
            numeratorInput.text = string.Empty;
            denominatorInput.text = string.Empty;
            foreach (var inputImage in inputImages)
            {
                inputImage.color = Color.white;
            }
            Answer = null;
        }

        private void SetAnswer(string value)
        {
            if (string.IsNullOrEmpty(wholeNumberInput.text) ||
                string.IsNullOrEmpty(numeratorInput.text) ||
                string.IsNullOrEmpty(denominatorInput.text)) return;

            var wholeNumber = int.Parse(wholeNumberInput.text);
            var numerator = int.Parse(numeratorInput.text);
            var denominator = int.Parse(denominatorInput.text);

            Answer = wholeNumber + decimal.Divide(numerator, denominator);
            OnAnswerSet?.Invoke(Answer.Value);
        }
    }
}