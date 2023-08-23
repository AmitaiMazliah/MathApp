using System;
using UnityEngine;

namespace MathApp
{
    [CreateAssetMenu(menuName = "Questions/Question")]
    public class MathQuestionV2 : ScriptableObject
    {
        public decimal? number1;
        public decimal? number2;
        public QuestionOperation operation;
        public decimal? answer;
    }
}