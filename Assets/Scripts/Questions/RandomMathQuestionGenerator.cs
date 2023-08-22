using System;

namespace MathApp
{
    public class RandomMathQuestionGenerator
    {
        private readonly Random random = new Random();
        
        public MathQuestion Generate()
        {
            var values = Enum.GetValues(typeof(QuestionOperation));
            var operation = (QuestionOperation)values.GetValue(random.Next(values.Length));
            return Generate(operation);
        }

        public MathQuestion Generate(QuestionOperation operation)
        {
            var num1 = random.Next(1, 100);
            var num2 = random.Next(1, 100);
            return new MathQuestion
            {
                operation = operation,
                number1 = num1,
                number2 = num2
            };
        }
    }
}