namespace libudev.Rules.Names
{
    /// <summary>
    /// Test the existence of a file. An octal mode mask can be specified if needed via <see cref="Token.Key"/>
    /// </summary>
    public class TEST : NamedToken
    {
        public TEST(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "TEST";
    }
}