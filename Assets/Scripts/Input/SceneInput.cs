using System.Collections.Generic;
using MathApp;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneInput : SceneService
{
    private List<IBackHandler> backHandlers = new List<IBackHandler>();

    private bool hasInput;

    EventSystem system;

    void Awake()
    {
        system = EventSystem.current;
    }

    void OnNextSelectable()
    {
        Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

        if (next != null)
        {
            InputField inputField = next.GetComponent<InputField>();
            if (inputField != null)
            {
                // if it's an input field, also set the text caret
                inputField.OnPointerClick(new PointerEventData(system));
            }

            system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
        }
    }

    void OnBackAction()
    {
        if (Context.hasInput || Scene is Menu)
        {
            BackAction();
        }
    }

    // SceneService INTERFACE

    protected override void OnTick()
    {
        base.OnTick();

        if (Context.hasInput && Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackAction();
        }
    }

    private void BackAction()
    {
        if (backHandlers.Count == 0)
        {
            Context.UI.GetAll(backHandlers);
            backHandlers.Add(Context.UI);

            backHandlers.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }

        for (int i = 0, count = backHandlers.Count; i < count; ++i)
        {
            IBackHandler handler = backHandlers[i];
            if (handler.IsActive && handler.OnBackAction())
                break;
        }
    }

    private void RefreshCursor()
    {
        if (IsActive == false)
            return;

        if (Context is { hasInput: false })
            return;

        // if (IsCursorVisible)
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        // }
        // else
        // {
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        // }
    }
}