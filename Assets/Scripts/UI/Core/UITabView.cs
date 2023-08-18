using UnityEngine;

namespace MathApp.UI
{
    public abstract class UITabView : UIView
    {
        [SerializeField] GameObject deselectedObject;
        [SerializeField] GameObject selectedObject;

        public UITabsView Container { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Container = Owner as UITabsView;
            if (selectedObject != null) selectedObject.SetActive(false);
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            if (deselectedObject != null) deselectedObject.SetActive(false);
            if (selectedObject != null) selectedObject.SetActive(true);
        }

        protected override void OnClose()
        {
            if (selectedObject != null) selectedObject.SetActive(false);
            if (deselectedObject != null) deselectedObject.SetActive(true);

            base.OnClose();
        }
    }
}
