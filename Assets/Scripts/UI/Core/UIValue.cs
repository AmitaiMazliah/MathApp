using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MathApp.UI
{
    public class UIValue : UIBehaviour
    {
        // PUBLIC MEMBERS

        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                showPercentage = value;
                formatInitialized = false;
            }
        }

        public int DecimalNumbers
        {
            get { return decimalNumbers; }
            set
            {
                decimalNumbers = value;
                formatInitialized = false;
            }
        }

        public bool ShowMaximum
        {
            get { return showMaximum; }
            set
            {
                showMaximum = value;
                formatInitialized = false;
            }
        }

        public float Value { get { return value; } }
        public float MaxValue { get { return maxValue; } }
        public new TextMeshProUGUI Text { get { return text; } }
        public Image Fill { get { return fill; } }

        // PRIVATE MEMBERS

        [SerializeField] private Image fill;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private bool showPercentage;
        [SerializeField] private bool showMaximum;
        [SerializeField] private int decimalNumbers;

        [SerializeField] [Tooltip("Example: \"Available in {0} seconds\"")]
        private string textFormat;

        [SerializeField] private string infinitySymbol = "~";
        [SerializeField] private float minChange = 0.01f;

        [Header("Time Setup")] [SerializeField]
        private bool displayInTimeFormat;

        [SerializeField] private bool showZeroHours;
        [SerializeField] private bool showZeroMinutes;
        [SerializeField] private bool showZeroSeconds;

        private float value = float.MinValue;
        private float maxValue;

        private bool formatInitialized;
        private string format;

        // PUBLIC METHODS

        public void SetValue(float value, float maxValue = 0.0f)
        {
            if (Mathf.Abs(this.value - value) < minChange && Mathf.Abs(this.maxValue - maxValue) < minChange)
                return;

            if (formatInitialized == false)
            {
                InitializeFormat();
            }

            this.value = value;
            this.maxValue = maxValue;

            if (text != null)
            {
                if (displayInTimeFormat)
                {
                    int hours = (int)(value / 3600);
                    int minutes = (int)(value / 60) - hours * 60;
                    int seconds = (int)(value % 60);

                    string timeString = string.Empty;

                    if (hours > 0 || showZeroHours)
                    {
                        timeString = $"{hours}:{minutes:00}:{seconds:00}";
                    }
                    else if (minutes > 0 || showZeroMinutes)
                    {
                        timeString = $"{minutes}:{seconds:00}";
                    }
                    else if (seconds > 0 || showZeroSeconds)
                    {
                        timeString = $"{seconds}";
                    }

                    text.text = string.Format(format, timeString);
                }
                else
                {
                    float textValue = showPercentage ? this.value / this.maxValue : this.value;

                    if (textValue < float.MaxValue && maxValue < float.MaxValue)
                    {
                        text.text = showMaximum
                            ? string.Format(format, textValue, maxValue)
                            : string.Format(format, textValue);
                    }
                    else
                    {
                        string stringValue = textValue < float.MaxValue ? textValue.ToString() : infinitySymbol;
                        string stringMaxValue = maxValue < float.MaxValue ? maxValue.ToString() : infinitySymbol;

                        text.text = showMaximum
                            ? string.Format(format, stringValue, stringMaxValue)
                            : string.Format(format, stringValue);
                    }
                }
            }

            if (fill != null)
            {
                if (fill.type == Image.Type.Filled)
                {
                    fill.fillAmount = this.value / (this.maxValue == 0.0f ? 1.0f : this.maxValue);
                }
                else
                {
                    fill.rectTransform.anchorMax = new Vector2(this.value / this.maxValue, fill.rectTransform.anchorMax.y);
                }
            }
        }

        public void SetFillColor(Color color)
        {
            fill.color = color;
        }

        // PRIVATE METHODS

        private void InitializeFormat()
        {
            if (text == null)
                return;

            if (displayInTimeFormat)
            {
                format = $"{{0}}";
            }
            else
            {
                string numberFormat = showPercentage ? "P" + decimalNumbers : "N" + decimalNumbers;

                if (showMaximum)
                {
                    format = $"{{0:{numberFormat}}} / {{1:{numberFormat}}}";
                }
                else
                {
                    format = $"{{0:{numberFormat}}}";
                }
            }

            format = textFormat.HasValue() ? string.Format(textFormat, format) : format;
            formatInitialized = true;
        }
    }
}
