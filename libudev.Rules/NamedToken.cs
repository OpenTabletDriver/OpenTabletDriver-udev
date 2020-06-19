namespace libudev.Rules
{
    public abstract class NamedToken : Token
    {
        protected NamedToken(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public abstract string Name { get; }

        public override string ToString() => $"{Name}{{{Key}}}" + GetFormat(Operator) + $"\"{Value}\"";
    }
}