using TMPro;
using UnityEngine;

namespace MathApp.UI
{
    public class UINotesView : UIWidget
    {
        [SerializeField] private TMP_InputField inputField;

        public void Clear()
        {
            inputField.text = string.Empty;
        }
    }
}