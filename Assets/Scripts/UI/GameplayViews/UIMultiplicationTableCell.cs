using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MathApp.UI
{
    public class UIMultiplicationTableCell : UIBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_InputField input;

        private int answer;

        private void OnEnable()
        {
            input.onEndEdit.AddListener(ValidateAnswer);
        }

        private void OnDisable()
        {
            input.onEndEdit.RemoveListener(ValidateAnswer);
        }

        public void SetValue(int value)
        {
            input.text = value.ToString();
            input.interactable = false;
        }

        public void SetAnswer(int value)
        {
            answer = value;
        }
        
        private void ValidateAnswer(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            
            if (int.Parse(value) == answer)
            {
                image.color = Color.green;
            }
            else
            {
                image.color = Color.red;
            }
        }
    }
}
