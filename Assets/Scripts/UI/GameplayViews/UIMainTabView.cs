using MathApp.Events;
using MathApp.SceneManagement;
using MathApp.UI;
using UnityEngine;

public class UIMainTabView : UITabView
{
    [SerializeField] private UIButton multiplicationTableButton;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        multiplicationTableButton.onClick.AddListener(ChangeScene);
    }

    protected override void OnDeinitialize()
    {
        multiplicationTableButton.onClick.RemoveListener(ChangeScene);

        base.OnDeinitialize();
    }

    protected override bool OnBackAction()
    {
        if (IsInteractable == false)
            return false;

        return true;
    }

    private void ChangeScene()
    {
        Switch<UIMultiplicationTableView>();
    }
}