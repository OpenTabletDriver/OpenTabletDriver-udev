namespace libudev.Rules.Names
{
    public class TEST : NamedToken
    {
        public TEST(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "TEST";
    }
}