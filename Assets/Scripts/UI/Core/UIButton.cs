using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using MathApp.Audio;
using UnityEngine.EventSystems;

namespace MathApp.UI
{
    public class UIButton : Button
    {
        // PRIVATE MEMBERS

        [SerializeField] private bool playClickSound = true;
        [SerializeField] UIActionSoundMapping customSoundEffects;

        private UIWidget parent;

        private static List<UIWidget> tempWidgetList = new List<UIWidget>(16);

        public void PlayActionSound(UIAction action)
        {
            if (playClickSound == false)
                return;

            if (parent == null)
            {
                tempWidgetList.Clear();

                GetComponentsInParent(true, tempWidgetList);

                parent = tempWidgetList.Count > 0 ? tempWidgetList[0] : null;
                tempWidgetList.Clear();
            }

            var customSound = customSoundEffects?.mapping?[action];

            if (customSound != null)
            {
                parent.PlaySound(customSound);
            }
            else
            {
                parent.PlayActionSound(action);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            onClick.AddListener(OnClick);

            if (transition == Transition.Animation)
            {
                var buttonAnimator = animator;
                if (buttonAnimator != null)
                {
                    buttonAnimator.keepAnimatorStateOnDisable = true;
                }
            }
        }

        protected override void OnDestroy()
        {
            onClick.RemoveListener(OnClick);

            base.OnDestroy();
        }

        private void OnClick()
        {
            PlayActionSound(UIAction.Click);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            PlayActionSound(UIAction.Hover);
        }
    }
}
