using System.Collections.Generic;

namespace MathApp
{
    public enum QuestionOperation
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public static class QuestionOperationExtensions
    {
        private static readonly Dictionary<QuestionOperation, char> mapping = new Dictionary<QuestionOperation, char>()
        {
            { QuestionOperation.Plus, '+' },
            { QuestionOperation.Minus, '-' },
            { QuestionOperation.Multiply, 'x' },
            { QuestionOperation.Divide, '÷' }
        };
        
        public static char DisplayName(this QuestionOperation value)
        {
            return mapping[value];
        }
    }
}