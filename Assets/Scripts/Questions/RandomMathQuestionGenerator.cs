using System;

namespace MathApp
{
    public static class RandomMathQuestionGenerator
    {
        private static readonly Random random = new Random();

        public static MathQuestion Generate(GenerateQuestionRequest request)
        {
            if (request.Operation == null)
            {
                var values = Enum.GetValues(typeof(QuestionOperation));
                request.Operation = (QuestionOperation)values.GetValue(random.Next(values.Length));
            }
            
            var num1 = random.Next(request.Number1Request.Min ?? 1, request.Number1Request.Max ?? 100);
            var num2 = random.Next(request.Number2Request.Min ?? 1, request.Number2Request.Max ?? 100);
            return new MathQuestion
            {
                operation = request.Operation.Value,
                number1 = num1,
                number2 = num2
            };
        }
    }

    public class GenerateQuestionRequest
    {
        public QuestionOperation? Operation { get; set; }
        public GenerateQuestionNumberRequest Number1Request { get; set; }
        public GenerateQuestionNumberRequest Number2Request { get; set; }
    }

    public class GenerateQuestionNumberRequest
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
}