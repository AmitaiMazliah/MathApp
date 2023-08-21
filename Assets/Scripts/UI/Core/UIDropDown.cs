using MathApp.Audio;
using MathApp.UI;
using TMPro;
using UnityEngine;

public class UIDropDown : TMP_Dropdown
{
    [SerializeField] private bool playValueChangedSound = true;

    [SerializeField] private AudioCueSO customValueChangedSound;

    private UIWidget parent;

    protected override void Awake()
    {
        base.Awake();

        if (Application.isPlaying)
        {
            onValueChanged.AddListener(OnValueChanged);
        }
    }

    protected override void OnDestroy()
    {
        onValueChanged.RemoveListener(OnValueChanged);

        base.OnDestroy();
    }

    private void OnValueChanged(int selectedValueIndex)
    {
        if (playValueChangedSound == false)
            return;

        if (parent == null)
        {
            parent = GetComponentInParent<UIWidget>();
        }

        if (parent == null)
            return;

        if (customValueChangedSound != null)
        {
            parent.PlaySound(customValueChangedSound);
        }
        else
        {
            parent.PlayActionSound(UIAction.Click);
        }
    }
}