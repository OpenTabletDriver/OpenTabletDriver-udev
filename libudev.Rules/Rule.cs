using System;
using System.Collections.Generic;
using System.Linq;

namespace libudev.Rules
{
    public class Rule
    {
        public Rule()
        {
        }

        public Rule(params Token[] tokens)
        {
            Tokens = tokens;
        }

        IEnumerable<Token> Tokens { set; get; }

        public override string ToString() => string.Join(", ", Tokens);
        public static implicit operator string(Rule rule) => rule.ToString();
    }
}
