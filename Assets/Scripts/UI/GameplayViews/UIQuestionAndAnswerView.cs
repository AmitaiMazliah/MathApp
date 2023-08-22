using MathApp.Audio;
using MathApp.Events;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UIQuestionAndAnswerView : UIBehaviour
    {
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_InputField answerInput;
        [SerializeField] private Image answerImage;
        
        [SerializeField] private AudioCueSO correctSound;
        [SerializeField] private AudioCueSO wrongSound;

        [SerializeField] private AudioCueEventChannelSO playSoundOn;
        [SerializeField] private AudioConfigurationSO audioConfig;

        private MathQuestion question;
        
        private void OnEnable()
        {
            answerInput.onEndEdit.AddListener(ValidateAnswer);
        }

        private void OnDisable()
        {
            answerInput.onEndEdit.RemoveListener(ValidateAnswer);
        }
        
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void SetQuestion(int num1, int num2, QuestionOperation operation)
        {
            SetQuestion(new MathQuestion
            {
                operation = operation,
                number1 = num1,
                number2 = num2
            });
        }

        public void SetQuestion(MathQuestion question)
        {
            questionText.text = $"{question.number1} {question.operation.DisplayName()} {question.number2} =";
            this.question = question;
        }
        
        private void ValidateAnswer(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            var answer = float.Parse(value);

            if (answer == question.Answer)
            {
                answerImage.color = Color.green;
                playSoundOn.RaisePlayEvent(correctSound, audioConfig);
            }
            else
            {
                answerImage.color = Color.red;
                playSoundOn.RaisePlayEvent(wrongSound, audioConfig);
            }
        }
    }
}