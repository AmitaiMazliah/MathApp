using MathApp.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIDivisionQuestionsView : UIView
{
    [SerializeField] private UIButton backButton;
    [SerializeField] private UIButton resetButton;

    [SerializeField] private UIQuestionAndAnswerView questionAndAnswerView;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        backButton.onClick.AddListener(Back);
        resetButton.onClick.AddListener(ResetAnswer);
    }

    protected override void OnDeinitialize()
    {
        backButton.onClick.RemoveListener(Back);
        resetButton.onClick.RemoveListener(ResetAnswer);

        base.OnDeinitialize();
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
        
    }
}