using System;
using System.Collections.Generic;
using MathApp;
using MathApp.Audio;
using MathApp.Events;
using MathApp.UI;
using UnityEngine;

public class SceneUI : SceneService, IBackHandler
{
    public Canvas Canvas { get; private set; }
    public Camera UICamera { get; private set; }

    [SerializeField] private UIView[] defaultViews;
    [Header("Audio")]
    [SerializeField] AudioCueEventChannelSO SFXEventChannel;
    [SerializeField] AudioConfigurationSO clickSoundConfig;
    [SerializeField] UIActionSoundMapping soundEffects;

    private ScreenOrientation lastScreenOrientation;
    
    protected UIView[] Views;

    protected virtual void OnInitializeInternal() { }
    protected virtual void OnDeinitializeInternal() { }
    protected virtual void OnTickInternal() { }
    protected virtual bool OnBackAction() { return false; }
    protected virtual void OnViewOpened(UIView view) { }
    protected virtual void OnViewClosed(UIView view) { }

    public T Get<T>() where T : UIView
    {
        if (Views == null)
            return null;

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i] as T;

            if (view != null)
                return view;
        }

        return null;
    }

    public T Open<T>() where T : UIView
    {
        if (Views == null)
            return null;

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i] as T;
            if (view != null)
            {
                OpenView(view);
                return view;
            }
        }

        return null;
    }

    public void Open(UIView view)
    {
        if (Views == null)
            return;

        var index = Array.IndexOf(Views, view);

        if (index < 0)
        {
            Debug.LogError($"Cannot find view {view.name}");
            return;
        }

        OpenView(view);
    }

    /*
    public T OpenWithBackView<T>(UIView backView) where T : UICloseView
    {
        T view = Open<T>();

        if (view is UICloseView closeView)
        {
            closeView.BackView = backView;
        }

        return view;
    }
    */

    public T Close<T>() where T : UIView
    {
        if (Views == null)
            return null;

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i] as T;
            if (view != null)
            {
                view.Close();
                return view;
            }
        }

        return null;
    }

    public void Close(UIView view)
    {
        if (Views == null)
            return;

        var index = Array.IndexOf(Views, view);

        if (index < 0)
        {
            Debug.LogError($"Cannot find view {view.name}");
            return;
        }

        CloseView(view);
    }

    public T Toggle<T>() where T : UIView
    {
        if (Views == null)
            return null;

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i] as T;
            if (view != null)
            {
                if (view.IsOpen)
                {
                    CloseView(view);
                }
                else
                {
                    OpenView(view);
                }

                return view;
            }
        }

        return null;
    }

    public bool IsOpen<T>() where T : UIView
    {
        if (Views == null)
            return false;

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i] as T;
            if (view != null)
            {
                return view.IsOpen;
            }
        }

        return false;
    }

    public bool IsTopView(UIView view, bool interactableOnly = false)
    {
        if (view.IsOpen == false)
            return false;

        if (Views == null)
            return false;

        var highestPriority = -1;

        for (var i = 0; i < Views.Length; ++i)
        {
            var otherView = Views[i];

            if (otherView == view)
                continue;

            if (otherView.IsOpen == false)
                continue;

            if (interactableOnly && otherView.IsInteractable == false)
                continue;

            highestPriority = Math.Max(highestPriority, otherView.Priority);
        }

        return view.Priority > highestPriority;
    }

    public void CloseAll()
    {
        if (Views == null)
            return;

        for (var i = 0; i < Views.Length; ++i)
        {
            CloseView(Views[i]);
        }
    }

    public void GetAll<T>(List<T> list)
    {
        if (Views == null)
            return;

        for (var i = 0; i < Views.Length; ++i)
        {
            if (Views[i] is T element)
            {
                list.Add(element);
            }
        }
    }

    public void PlaySound(AudioCueSO sound)
    {
        SFXEventChannel.RaisePlayEvent(sound, clickSoundConfig);
    }

    public void PlayActionSound(UIAction action)
    {
        var sound = soundEffects.mapping[action];
        if (sound != null)
        {
            SFXEventChannel.RaisePlayEvent(sound, clickSoundConfig);
        }
    }

    // IBackHandler INTERFACE

    int IBackHandler.Priority => -1;
    bool IBackHandler.IsActive => true;
    bool IBackHandler.OnBackAction() { return OnBackAction(); }

    // GameService INTERFACE

    protected override sealed void OnInitialize()
    {
        Canvas = GetComponent<Canvas>();
        UICamera = Canvas.worldCamera;
        Views = GetComponentsInChildren<UIView>(true);

        for (var i = 0; i < Views.Length; ++i)
        {
            var view = Views[i];

            view.Initialize(this, null);
            view.SetPriority(i);

            view.gameObject.SetActive(false);
        }

        OnInitializeInternal();

        UpdateScreenOrientation();
    }

    protected override sealed void OnDeinitialize()
    {
        OnDeinitializeInternal();

        if (Views != null)
        {
            for (var i = 0; i < Views.Length; ++i)
            {
                Views[i].Deinitialize();
            }

            Views = null;
        }
    }

    protected override void OnActivate()
    {
        // if (ApplicationSettings.IsStrippedBatch)
        // {
        //     Canvas.enabled = false;
        //     return;
        // }

        base.OnActivate();

        Canvas.enabled = true;

        for (int i = 0, count = defaultViews.SafeCount(); i < count; i++)
        {
            Open(defaultViews[i]);
        }
    }

    protected override void OnDeactivate()
    {
        // if (ApplicationSettings.IsStrippedBatch)
        //     return;

        base.OnDeactivate();

        for (int i = 0, count = Views.SafeCount(); i < count; i++)
        {
            Close(Views[i]);
        }

        if (Canvas != null)
        {
            Canvas.enabled = false;
        }
    }

    protected override sealed void OnTick()
    {
        // if (ApplicationSettings.IsStrippedBatch)
        //     return;

        UpdateScreenOrientation();

        if (Views != null)
        {
            for (var i = 0; i < Views.Length; ++i)
            {
                var view = Views[i];
                if (view.IsOpen)
                {
                    view.Tick();
                }
            }
        }

        OnTickInternal();
    }

    // PRIVATE MEMBERS

    private void UpdateScreenOrientation()
    {
        if (lastScreenOrientation == Screen.orientation)
            return;

        if (Views != null)
        {
            for (var i = 0; i < Views.Length; ++i)
            {
                Views[i].UpdateSafeArea();
            }

            lastScreenOrientation = Screen.orientation;
        }
    }

    private void OpenView(UIView view)
    {
        if (view == null)
            return;

        if (view.IsOpen)
            return;

        view.Open_Internal();

        OnViewOpened(view);
    }

    private void CloseView(UIView view)
    {
        if (view == null)
            return;

        if (view.IsOpen == false)
            return;

        view.Close_Internal();

        OnViewClosed(view);
    }
}
