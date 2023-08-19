using UnityEngine;

namespace MathApp.UI
{
    public class UIMultiplicationTableView : UIView
    {
        [SerializeField] private UIList t;
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            t.UpdateContent += OnListUpdateContent;
        }

        protected override void OnDeinitialize()
        {
            t.UpdateContent -= OnListUpdateContent;

            base.OnDeinitialize();
        }
        
        protected override void OnOpen()
        {
            t.Refresh(121, false);
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
    }
}
