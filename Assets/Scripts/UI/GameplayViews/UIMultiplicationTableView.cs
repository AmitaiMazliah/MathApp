using System.Collections;
using System.Linq;
using MathApp.UI;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class UIMultiplicationTableView : UITabView
{
    [SerializeField] private UIList tableCells;
    [SerializeField] private UIButton backButton;
    [SerializeField] private UIButton resetButton;

    [SerializeField] private ParticleSystem confettiEffect;

    private bool completed;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        tableCells.UpdateContent += OnListUpdateContent;
        backButton.onClick.AddListener(Back);
        resetButton.onClick.AddListener(ResetTable);
    }

    protected override void OnDeinitialize()
    {
        tableCells.UpdateContent -= OnListUpdateContent;
        backButton.onClick.RemoveListener(Back);
        resetButton.onClick.RemoveListener(ResetTable);

        base.OnDeinitialize();
    }

    protected override void OnOpen()
    {
        tableCells.Refresh(121, false);
    }

    protected override void OnClose()
    {
        ResetTable();
        completed = false;
    }

    protected override void OnTick()
    {
        base.OnTick();

        if (!completed && tableCells.Items.OfType<UIMultiplicationTableCell>()
                .Where(c => c.Interactable).All(c => c.Correct))
        {
            completed = true;
            StartCoroutine(Celebrate());
        }
    }

    private IEnumerator Celebrate()
    {
        confettiEffect.Play();
        yield return new WaitForSeconds(2f);
        confettiEffect.Stop();
    }

    void OnListUpdateContent(int index, MonoBehaviour content)
    {
        var cell = content as UIMultiplicationTableCell;
        var column = index / 11;
        var row = index % 11;
        if (column == 0)
        {
            cell.SetValue(row);
        }
        else if (row == 0)
        {
            cell.SetValue(column);
        }
        else
        {
            cell.SetAnswer(row * column);
        }
    }

    private void Back()
    {
        Switch<UIMainTabView>();
    }

    private void ResetTable()
    {
        foreach (var tableCellsItem in tableCells.Items)
        {
            var cell = tableCellsItem as UIMultiplicationTableCell;
            if (cell.Interactable) cell.Clear();
        }
    }

    [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
    private void CompleteTable()
    {
        tableCells.Items.OfType<UIMultiplicationTableCell>().Where(c => c.Interactable)
            .ForEach(c => c.Complete());
    }
}