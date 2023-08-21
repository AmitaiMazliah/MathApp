using MathApp.Audio;
using MathApp.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UIMultiplicationTableCell : UIBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_InputField input;

        [SerializeField] private AudioCueSO correctSound;
        [SerializeField] private AudioCueSO wrongSound;
        
        [SerializeField] AudioCueEventChannelSO playSoundOn;
        [SerializeField] AudioConfigurationSO audioConfig;

        private int? answer;
        private int correctAnswer;

        public bool Interactable => input.interactable;
        public bool Correct => answer == correctAnswer;

        private void OnEnable()
        {
            input.onEndEdit.AddListener(ValidateAnswer);
        }

        private void OnDisable()
        {
            input.onEndEdit.RemoveListener(ValidateAnswer);
        }

        public void SetValue(int value)
        {
            input.text = value.ToString();
            input.interactable = false;
        }

        public void SetAnswer(int value)
        {
            correctAnswer = value;
        }

        public void Clear()
        {
            input.text = string.Empty;
            image.color = Color.white;
        }

        public void Complete()
        {
            input.text = correctAnswer.ToString();
            ValidateAnswer(input.text);
        }
        
        private void ValidateAnswer(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            answer = int.Parse(value);
            
            if (Correct)
            {
                image.color = Color.green;
                playSoundOn.RaisePlayEvent(correctSound, audioConfig);
            }
            else
            {
                image.color = Color.red;
                playSoundOn.RaisePlayEvent(wrongSound, audioConfig);
            }
        }
    }
}
