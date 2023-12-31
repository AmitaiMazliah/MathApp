using System;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public interface IDelayBlurView
    {
        int DelayFrames { get; }
    }

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIView : UIWidget, IBackHandler
    {
        public event Action HasOpened;
        public event Action HasClosed;

        public bool IsOpen { get; private set; }

        public bool IsInteractable
        {
            get { return CanvasGroup.interactable; }
            set { CanvasGroup.interactable = value; }
        }

        public int Priority => priority;
        public virtual bool NeedsCursor => needsCursor;

        // PRIVATE MEMBERS

        [SerializeField] private bool canHandleBackAction;
        [SerializeField] private bool needsCursor;
        [SerializeField] private bool useSafeArea = true;

        private int priority;

        private Rect lastSafeArea;

        // PUBLIC METHODS

        public void Open()
        {
            SceneUI.Open(this);
        }

        public void Close()
        {
            if (SceneUI == null)
            {
                Debug.Log($"Closing view {gameObject.name} without SceneUI");
                Close_Internal();
            }
            else
            {
                SceneUI.Close(this);
            }
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }

        public void SetState(bool isOpen)
        {
            if (isOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public void UpdateSafeArea()
        {
            if (useSafeArea == false)
                return;

            Rect safeArea = Screen.safeArea;
            if (safeArea == lastSafeArea)
                return;

            ApplySafeArea(safeArea);
        }

        public bool IsTopView(bool interactableOnly = false)
        {
            return SceneUI.IsTopView(this, interactableOnly);
        }

        // UIWidget INTERFACE

        protected override void OnInitialize()
        {
        }

        protected override void OnDeinitialize()
        {
            Close_Internal();

            HasOpened = null;
            HasClosed = null;
        }

        public void Tick(float deltaTime)
        {
        }

        // INTERNAL METHODS

        internal void Open_Internal()
        {
            if (IsOpen)
                return;

            IsOpen = true;

            gameObject.SetActive(true);

            OnOpen();

            //	RenderBlur();

            if (HasOpened != null)
            {
                HasOpened();
                HasOpened = null;
            }
        }

        internal void Close_Internal()
        {
            if (IsOpen == false)
                return;

            IsOpen = false;

            OnClose();

            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }

            //	ClearBlur();

            if (HasClosed != null)
            {
                HasClosed();
                HasClosed = null;
            }
        }

        // IBackHandler INTERFACE

        int IBackHandler.Priority => priority;
        bool IBackHandler.IsActive => IsOpen && canHandleBackAction;
        bool IBackHandler.OnBackAction() { return OnBackAction(); }

        // UIView INTERFACE

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }

        protected virtual bool OnBackAction()
        {
            if (IsInteractable)
            {
                Close();
            }

            return true;
        }

        // PROTECTED METHODS

        protected T Switch<T>() where T : UIView
        {
            Close();

            return SceneUI.Open<T>();
        }

        protected T Open<T>() where T : UIView
        {
            return SceneUI.Open<T>();
        }

        protected void Open(UIView view)
        {
            SceneUI.Open(view);
        }
        
        protected T Toggle<T>() where T : UIView
        {
            return SceneUI.Toggle<T>();
        }

        /*
        protected T OpenWithBackView<T>() where T : UICloseView
        {
            return SceneUI.OpenWithBackView<T>(this);
        }

        protected T OpenWithBackView<T>(UIView backView) where T : UICloseView
        {
            return SceneUI.OpenWithBackView<T>(backView);
        }
        */

        // PRIVATE METHODS

        private void ApplySafeArea(Rect safeArea)
        {
            RectTransform rectTransform = transform as RectTransform;

            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            int screenHeight = Screen.height;
            int screenWidth = Screen.width;
            anchorMin.x /= screenWidth;
            anchorMax.x /= screenWidth;
            anchorMin.y /= screenHeight;
            anchorMax.y /= screenHeight;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            lastSafeArea = safeArea;
        }

        /*
        private void RenderBlur()
        {
            if (_blurImage == null)
                return;

            CanvasGroup.alpha = 0f;

            int blurDelay = this is IDelayBlurView blurView ? blurView.DelayFrames : 0;
            _blurImage.RenderBlur(this.GetType(), RenderBlurFinished, blurDelay);
        }

        private void RenderBlurFinished()
        {
            CanvasGroup.alpha = 1f;
        }

        private void ClearBlur()
        {
            if (_blurImage == null)
                return;

            _blurImage.ClearBlur(this.GetType());
        }
        */
    }
}
