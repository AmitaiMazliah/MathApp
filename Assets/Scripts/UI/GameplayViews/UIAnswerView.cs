using System;
using System.Collections.Generic;
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
            var list = new List<Image>();
            if (wholeNumberInput != null) list.Add(wholeNumberInput.GetComponent<Image>());
            if (numeratorInput != null) list.Add(numeratorInput.GetComponent<Image>());
            if (denominatorInput != null) list.Add(denominatorInput.GetComponent<Image>());
            
            inputImages = list.ToArray();
        }

        private void OnEnable()
        {
            wholeNumberInput?.onEndEdit.AddListener(SetAnswer);
            numeratorInput?.onEndEdit.AddListener(SetAnswer);
            denominatorInput?.onEndEdit.AddListener(SetAnswer);
        }

        private void OnDisable()
        {
            wholeNumberInput?.onEndEdit.RemoveListener(SetAnswer);
            numeratorInput?.onEndEdit.RemoveListener(SetAnswer);
            denominatorInput?.onEndEdit.RemoveListener(SetAnswer);
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
            if (numeratorInput != null) numeratorInput.text = string.Empty;
            if (denominatorInput != null) denominatorInput.text = string.Empty;
            foreach (var inputImage in inputImages)
            {
                inputImage.color = Color.white;
            }
            Answer = null;
        }

        private void SetAnswer(string value)
        {
            Answer = GetAnswerFromInput();
            Debug.Log(Answer);
            if (Answer.HasValue) OnAnswerSet?.Invoke(Answer.Value);
        }

        private decimal? GetAnswerFromInput()
        {
            var parsedWholeNumber = int.TryParse(wholeNumberInput.text, out var wholeNumber);
            var parsedNumerator = int.TryParse(numeratorInput?.text, out var numerator);
            var parsedDenominator = int.TryParse(denominatorInput?.text, out var denominator);

            if (!parsedWholeNumber ||
                numeratorInput != null && !parsedNumerator ||
                denominatorInput != null && !parsedDenominator) return null;

            if (denominator != 0) return wholeNumber + decimal.Divide(numerator, denominator);
            
            return wholeNumber;
        }
    }
}