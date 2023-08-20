using System.Collections.Generic;
using MathApp.Audio;
using MathApp.Events;
using UnityEngine;

namespace MathApp.UI
{
    public abstract class UIWidget : UIBehaviour
    {
        public bool IsVisible { get; private set; }

        protected bool IsInitalized { get; private set; }
        protected SceneUI SceneUI { get; private set; }
        protected SceneContext Context { get { return SceneUI.Context; } }
        protected UIWidget Owner { get; private set; }

        private List<UIWidget> children = new List<UIWidget>(16);

        public void PlayActionSound(UIAction action)
        {
            if (SceneUI != null)
            {
                SceneUI.PlayActionSound(action);
            }
        }

        public void PlaySound(AudioCueSO sound)
        {
        	if (sound == null)
        	{
        		Debug.LogWarning($"Missing click sound, parent {name}");
        		return;
        	}

        	if (SceneUI != null)
        	{
        		SceneUI.PlaySound(sound);
        	}
        }

        internal void Initialize(SceneUI sceneUI, UIWidget owner)
        {
            if (IsInitalized)
                return;

            SceneUI = sceneUI;
            Owner = owner;

            children.Clear();
            GetChildWidgets(transform, children);

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Initialize(sceneUI, this);
            }

            OnInitialize();

            IsInitalized = true;

            if (gameObject.activeInHierarchy)
            {
                Visible();
            }
        }

        internal void Deinitialize()
        {
            if (IsInitalized == false)
                return;

            Hidden();

            OnDeinitialize();

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Deinitialize();
            }

            children.Clear();

            IsInitalized = false;

            SceneUI = null;
            Owner = null;
        }

        internal void Visible()
        {
            if (IsInitalized == false)
                return;

            if (IsVisible)
                return;

            if (gameObject.activeSelf == false)
                return;

            IsVisible = true;

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Visible();
            }

            OnVisible();
        }

        internal void Hidden()
        {
            if (IsVisible == false)
                return;

            IsVisible = false;

            OnHidden();

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Hidden();
            }
        }

        internal void Tick()
        {
            if (IsInitalized == false)
                return;

            if (IsVisible == false)
                return;

            OnTick();

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Tick();
            }
        }

        internal void AddChild(UIWidget widget)
        {
            if (widget == null || widget == this)
                return;

            if (children.Contains(widget))
            {
                Debug.LogError($"Widget {widget.name} is already added as child of {name}");
                return;
            }

            children.Add(widget);

            widget.Initialize(SceneUI, this);
        }

        internal void RemoveChild(UIWidget widget)
        {
            int childIndex = children.IndexOf(widget);

            if (childIndex < 0)
            {
                Debug.LogError($"Widget {widget.name} is not child of {name} and cannot be removed");
                return;
            }

            widget.Deinitialize();

            children.RemoveAt(childIndex);
        }
        
        protected void OnEnable()
        {
            Visible();
        }

        protected void OnDisable()
        {
            Hidden();
        }
        
        public virtual bool IsActive() { return true; }

        protected virtual void OnInitialize() { }
        protected virtual void OnDeinitialize() { }
        protected virtual void OnVisible() { }
        protected virtual void OnHidden() { }
        protected virtual void OnTick() { }

        private static void GetChildWidgets(Transform transform, List<UIWidget> widgets)
        {
            foreach (Transform child in transform)
            {
                var childWidget = child.GetComponent<UIWidget>();

                if (childWidget != null)
                {
                    widgets.Add(childWidget);
                }
                else
                {
                    // Continue searching deeper in hierarchy
                    GetChildWidgets(child, widgets);
                }
            }
        }
    }
}
