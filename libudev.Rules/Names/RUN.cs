namespace libudev.Rules.Names
{
    public class RUN : NamedToken
    {
        public RUN(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "RUN";
    }
}