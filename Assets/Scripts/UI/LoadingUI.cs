using TMPro;
using UnityEngine;

namespace MathApp.UI
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] TMP_Text hint;
        [SerializeField, TextArea] string[] hints;

        void OnEnable()
        {
            hint.text = hints.GetRandom();
        }
    }
}
