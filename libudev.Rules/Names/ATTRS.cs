namespace libudev.Rules.Names
{
    public class ATTRS : NamedToken
    {
        public ATTRS(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "ATTRS";
    }
}