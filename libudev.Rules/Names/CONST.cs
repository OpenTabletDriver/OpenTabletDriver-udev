namespace libudev.Rules.Names
{
    public class CONST : NamedToken
    {
        public CONST(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "CONST";
    }
}