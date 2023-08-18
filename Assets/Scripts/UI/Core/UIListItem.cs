using System;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public sealed class UIListItem : UIListItemBase<MonoBehaviour>
    {
    }

    public abstract class UIListItemBase<T> : UIBehaviour where T : MonoBehaviour
    {
        public int ID { get; set; }
        public T Content => content;
        public bool IsSelectable => button != null;
        public bool IsSelected { get { return isSelected; } set { SetIsSelected(value); } }
        public bool IsInteractable { get { return GetIsInteractable(); } set { SetIsInteractable(value); } }

        public Action<int> Clicked;

        // PRIVATE MEMBERS

        [SerializeField] private Button button;
        [SerializeField] private Animator animator;
        [SerializeField] private T content;
        [SerializeField] private string selectedAnimatorParameter = "IsSelected";
        [SerializeField] private CanvasGroup selectedGroup;
        [SerializeField] private CanvasGroup deselectedGroup;

        private bool isSelected;

        protected virtual void Awake()
        {
            SetIsSelected(false, true);

            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }

            if (button != null && button.transition == Selectable.Transition.Animation)
            {
                animator = button.animator;
            }
        }

        protected virtual void OnDestroy()
        {
            Clicked = null;

            if (button != null)
            {
                button.onClick.RemoveListener(OnClick);
            }
        }

        // PRIVATE METHODS

        private void SetIsSelected(bool value, bool force = false)
        {
            if (isSelected == value && force == false)
                return;

            isSelected = value;

            selectedGroup.SetVisibility(value);
            deselectedGroup.SetVisibility(value == false);

            UpdateAnimator();
        }

        private bool GetIsInteractable()
        {
            return button != null ? button.interactable : false;
        }

        private void SetIsInteractable(bool value)
        {
            if (button == null)
                return;

            button.interactable = value;
        }

        private void OnClick()
        {
            Clicked?.Invoke(ID);
        }

        private void UpdateAnimator()
        {
            if (animator == null)
                return;

            if (selectedAnimatorParameter.HasValue() == false)
                return;

            if (animator != null)
            {
                animator.SetBool(selectedAnimatorParameter, isSelected);
            }
        }
    }
}
