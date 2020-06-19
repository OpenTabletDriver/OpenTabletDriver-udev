namespace libudev.Rules.Names
{
    public class ATTR : NamedToken
    {
        public ATTR(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "ATTR";
    }
}