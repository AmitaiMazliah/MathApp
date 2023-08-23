using MathApp.Events;
using MathApp.SceneManagement;
using MathApp.UI;
using UnityEngine;

public class UIMainTabView : UITabView
{
    [SerializeField] private UIButton multiplicationTableButton;
    [SerializeField] private UIButton multiplicationQuestionsButton;
    [SerializeField] private UIButton divideQuestionsButton;
    
    [SerializeField] private GameSceneSO multiplicationQuestionsScene;
    [SerializeField] private GameSceneSO divideQuestionsScene;
    [SerializeField] private LoadEventChannelSO loadSceneEvent;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        multiplicationTableButton.onClick.AddListener(OnMultiplicationTableButtonClick);
        multiplicationQuestionsButton.onClick.AddListener(() => ChangeScene(multiplicationQuestionsScene));
        divideQuestionsButton.onClick.AddListener(() => ChangeScene(divideQuestionsScene));
    }

    protected override void OnDeinitialize()
    {
        multiplicationTableButton.onClick.RemoveListener(OnMultiplicationTableButtonClick);
        multiplicationQuestionsButton.onClick.AddListener(() => ChangeScene(multiplicationQuestionsScene));
        divideQuestionsButton.onClick.RemoveListener(() => ChangeScene(divideQuestionsScene));

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
    
    private void ChangeScene(GameSceneSO scene)
    {
        loadSceneEvent.RaiseEvent(scene, true);
    }
}