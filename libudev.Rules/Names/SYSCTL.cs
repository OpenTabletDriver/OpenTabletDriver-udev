namespace libudev.Rules.Names
{
    public class SYSCTL : NamedToken
    {
        public SYSCTL(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "SYSCTL";
    }
}