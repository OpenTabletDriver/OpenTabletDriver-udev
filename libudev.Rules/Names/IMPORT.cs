namespace libudev.Rules.Names
{
    /// <summary>
    /// Import a set of variables as device properties, depending on <see cref="Token.Key"/>.\
    /// Supported keys: `program`, `builtin`, `file`, `db`, `cmdline`, `parent`
    /// </summary>
    public class IMPORT : NamedToken
    {
        public IMPORT(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "IMPORT";
    }
}