using UnityEngine;
using UnityEngine.Serialization;

namespace MathApp.UI
{
    public class UIMultiplicationTableView : UITabView
    {
        [SerializeField] private UIList tableCells;
        [SerializeField] private UIButton resetButton;
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            tableCells.UpdateContent += OnListUpdateContent;
            resetButton.onClick.AddListener(ResetTable);
        }

        protected override void OnDeinitialize()
        {
            tableCells.UpdateContent -= OnListUpdateContent;
            resetButton.onClick.RemoveListener(ResetTable);

            base.OnDeinitialize();
        }
        
        protected override void OnOpen()
        {
            tableCells.Refresh(121, false);
        }

        protected override void OnClose()
        {
            // Context.Announcer.Announce -= OnAnnounce;
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

        private void ResetTable()
        {
            foreach (var tableCellsItem in tableCells.Items)
            {
                var cell = tableCellsItem as UIMultiplicationTableCell;
                if (cell.Interactable) cell.Clear();
            }
        }
    }
}
