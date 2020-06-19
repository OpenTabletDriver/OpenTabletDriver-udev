namespace libudev.Rules.Names
{
    /// <summary>
    /// Match against a system-wide constant, depending on <see cref="Token.Key"/>.\
    /// Supported keys: `arch`, `virt`
    /// </summary>
    public class CONST : NamedToken
    {
        public CONST(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "CONST";
    }
}