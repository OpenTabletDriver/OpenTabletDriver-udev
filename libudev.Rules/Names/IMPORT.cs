namespace libudev.Rules.Names
{
    public class IMPORT : NamedToken
    {
        public IMPORT(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "IMPORT";
    }
}