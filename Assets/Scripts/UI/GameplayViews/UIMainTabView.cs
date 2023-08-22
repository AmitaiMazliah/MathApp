using MathApp.Events;
using MathApp.SceneManagement;
using MathApp.UI;
using UnityEngine;

public class UIMainTabView : UITabView
{
    [SerializeField] private UIButton multiplicationTableButton;
    [SerializeField] private UIButton divideQuestionsButton;
    
    [SerializeField] private GameSceneSO divideQuestionsScene;
    [SerializeField] private LoadEventChannelSO loadSceneEvent;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        multiplicationTableButton.onClick.AddListener(OnMultiplicationTableButtonClick);
        divideQuestionsButton.onClick.AddListener(ChangeScene);
    }

    protected override void OnDeinitialize()
    {
        multiplicationTableButton.onClick.RemoveListener(OnMultiplicationTableButtonClick);
        divideQuestionsButton.onClick.RemoveListener(ChangeScene);

        base.OnDeinitialize();
    }

    protected override bool OnBackAction()
    {
        if (IsInteractable == false)
            return false;

        return true;
    }

    private void OnMultiplicationTableButtonClick()
    {
        Switch<UIMultiplicationTableView>();
    }
    
    private void ChangeScene()
    {
        loadSceneEvent.RaiseEvent(divideQuestionsScene, true);
    }
}