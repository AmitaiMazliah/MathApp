using UnityEngine;
using System;
using System.Collections.Generic;

namespace MathApp.UI
{
    public class UIList : UIListBase<UIListItem, MonoBehaviour>
    {
    }

    public abstract class UIListBase<TListItem, TRContent> : UIBehaviour
        where TListItem : UIListItemBase<TRContent>
        where TRContent : MonoBehaviour
    {
        public event Action<int, TRContent> UpdateContent;
        public event Action<int> SelectionChanged;

        public int Selection
        {
            get => selection;
            set => SetSelection(value, true);
        }
        public int Count => dataCount;

        [SerializeField] private bool allowSelection = true;
        [SerializeField] private bool allowDeselection = true;
        [SerializeField] private TListItem itemInstance;

        private List<TListItem> items = new List<TListItem>(32);

        private int dataCount;
        private int selection = -1;

        public void Refresh(int dataCount, bool notifySelection = true)
        {
            this.dataCount = dataCount;

            UpdateItems();

            if (selection >= this.dataCount)
            {
                SetSelection(allowDeselection == false && this.dataCount > 0 ? 0 : -1, notifySelection, true);
            }
            else if (selection < 0 && allowDeselection == false && this.dataCount > 0)
            {
                SetSelection(0, notifySelection, true);
            }
            else
            {
                SetSelection(selection, false, true);
            }
        }

        public void Clear(bool destroyItems = true)
        {
            dataCount = 0;

            if (destroyItems)
            {
                itemInstance.SetActive(false);

                for (int i = 1; i < items.Count; i++)
                {
                    Destroy(items[i].gameObject);
                }

                items.Clear();
            }
            else
            {
                UpdateItems();
            }

            if (selection >= 0)
            {
                SetSelection(-1, true);
            }
        }

        // MONOBEHAVIOR

        protected void Awake()
        {
            if (dataCount == 0)
            {
                itemInstance.SetActive(false);
            }
        }

        // PRIVATE METHODS

        private void SetSelection(int selection, bool notify, bool force = false)
        {
            if (allowSelection == false)
                return;

            if (selection >= dataCount)
                return;

            if (selection == this.selection && force == false)
                return;

            if (selection < 0)
            {
                selection = -1;
            }

            this.selection = selection;

            for (int i = 0; i < dataCount; i++)
            {
                var item = items[i];
                item.IsSelected = item.ID == this.selection;
            }

            if (notify)
            {
                SelectionChanged?.Invoke(this.selection);
            }
        }

        private void UpdateItems()
        {
            bool selectable = itemInstance.IsSelectable;
            var parent = itemInstance.transform.parent;

            for (int i = items.Count; i < dataCount; i++)
            {
                var newItem = i == 0 ? itemInstance : Instantiate(itemInstance, parent);

                newItem.ID = i;

                if (selectable)
                {
                    newItem.Clicked -= OnItemClicked;
                    newItem.Clicked += OnItemClicked;
                }

                items.Add(newItem);
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (i < dataCount)
                {
                    UpdateContent?.Invoke(i, item.Content);
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }

        private void OnItemClicked(int id)
        {
            if (id == selection && allowDeselection == false)
                return;

            SetSelection(id == selection ? -1 : id, true);
        }
    }
}
