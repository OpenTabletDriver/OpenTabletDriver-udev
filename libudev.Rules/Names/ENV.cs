namespace libudev.Rules.Names
{
    public class ENV : NamedToken
    {
        public ENV(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "ENV";
    }
}