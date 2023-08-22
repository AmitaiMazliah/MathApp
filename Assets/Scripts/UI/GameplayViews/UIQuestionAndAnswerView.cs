using MathApp.Audio;
using MathApp.Events;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MathApp.UI
{
    public class UIQuestionAndAnswerView : UIBehaviour
    {
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private UIAnswerView answerView;
        
        [SerializeField] private AudioCueSO correctSound;
        [SerializeField] private AudioCueSO wrongSound;

        [SerializeField] private AudioCueEventChannelSO playSoundOn;
        [SerializeField] private AudioConfigurationSO audioConfig;

        private MathQuestion question;
        
        private void OnEnable()
        {
            answerView.OnAnswerSet += ValidateAnswer;
        }

        private void OnDisable()
        {
            answerView.OnAnswerSet -= ValidateAnswer;
        }

        public void ResetAnswer()
        {
            answerView.Clear();
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
        
        private void ValidateAnswer(decimal answer)
        {
            if (answer == question.Answer)
            {
                playSoundOn.RaisePlayEvent(correctSound, audioConfig);
                answerView.SetCorrect(true);
            }
            else
            {
                playSoundOn.RaisePlayEvent(wrongSound, audioConfig);
                answerView.SetCorrect(false);
            }
        }
    }
}