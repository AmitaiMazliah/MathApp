using System;

namespace MathApp
{
    [Serializable]
    public class MathQuestion
    {
        public int number1;
        public int number2;
        public QuestionOperation operation;

        public float Answer
        {
            get
            {
                return operation switch
                {
                    QuestionOperation.Plus => number1 + number2,
                    QuestionOperation.Minus => number1 - number2,
                    QuestionOperation.Multiply => number1 * number2,
                    QuestionOperation.Divide => number1 / number2,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}