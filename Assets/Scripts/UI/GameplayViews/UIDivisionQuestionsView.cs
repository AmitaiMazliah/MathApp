using MathApp;
using MathApp.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIDivisionQuestionsView : UIView
{
    [SerializeField] private UIButton backButton;
    [SerializeField] private UIButton resetButton;
    [SerializeField] private UIButton nextQuestionButton;

    [SerializeField] private UIQuestionAndAnswerView questionAndAnswerView;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        backButton.onClick.AddListener(Back);
        resetButton.onClick.AddListener(ResetAnswer);
        nextQuestionButton.onClick.AddListener(GenerateQuestion);
    }

    protected override void OnDeinitialize()
    {
        backButton.onClick.RemoveListener(Back);
        resetButton.onClick.RemoveListener(ResetAnswer);
        nextQuestionButton.onClick.RemoveListener(GenerateQuestion);

        base.OnDeinitialize();
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        GenerateQuestion();
    }

    private void Back()
    {
    }

    private void ResetAnswer()
    {
        questionAndAnswerView.ResetAnswer();
    }
    
    [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
    private void GenerateQuestion()
    {
        var question = RandomMathQuestionGenerator.Generate(new GenerateQuestionRequest
        {
            Operation = QuestionOperation.Divide,
            Number1Request = new GenerateQuestionNumberRequest
            {
                Min = 10,
                Max = 101
            },
            Number2Request = new GenerateQuestionNumberRequest
            {
                Min = 2,
                Max = 10
            }
        });
        questionAndAnswerView.SetQuestion(question);
    }
}