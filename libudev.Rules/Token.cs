using System;

namespace libudev.Rules
{
    public class Token
    {
        public Token(string key, Operator op, string value)
        {
            Key = key;
            Operator = op;
            Value = value;
        }

        public string Key { set; get; }
        public Operator Operator { set; get; }
        public string Value { set; get; }

        public override string ToString()
        {
            return Key + GetFormat(Operator) + $"\"{Value}\"";
        }

        protected static string GetFormat(Operator value)
        {
            switch (value)
            {
                case Operator.Equal:
                    return "==";
                case Operator.Inequal:
                    return "!=";
                case Operator.Assign:
                    return "=";
                case Operator.Add:
                    return "+=";
                case Operator.Remove:
                    return "-=";
                case Operator.AssignFinal:
                    return ":=";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}