using System;
using System.Linq;
using UnityEngine;

namespace MathApp.UI
{
    public abstract class UITabsView : UIView
    {
        [SerializeField] TabButton[] tabButtons;
        [SerializeField] UITabView defaultTab;

        UITabView currentTab;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (var tabButton in tabButtons)
            {
                tabButton.button.onClick.AddListener(() => SwitchTab(tabButton.tab));
            }
        }

        protected override void OnDeinitialize()
        {
            foreach (var tabButton in tabButtons)
            {
                tabButton.button.onClick.RemoveAllListeners();
            }

            base.OnDeinitialize();
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            tabButtons.FirstOrDefault()?.button?.Select();

            Open(defaultTab);
            currentTab = defaultTab;
        }

        public T SwitchTab<T>() where T : UITabView
        {
            SceneUI.Close(currentTab);
            var tab = Open<T>();
            currentTab = tab;
            return tab;
        }

        public void SwitchTab(UITabView tab)
        {
            SceneUI.Close(currentTab);
            Open(tab);
            currentTab = tab;
        }
    }

    [Serializable]
    public class TabButton
    {
        public UIButton button;
        public UITabView tab;
    }
}
