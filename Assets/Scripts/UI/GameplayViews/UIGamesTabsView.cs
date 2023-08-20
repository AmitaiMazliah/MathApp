using MathApp.Events;
using MathApp.SceneManagement;
using MathApp.UI;
using UnityEngine;

public class UIGamesTabsView : UITabsView
{
    protected override bool OnBackAction()
    {
        if (IsInteractable == false)
            return false;

        return true;
    }
}