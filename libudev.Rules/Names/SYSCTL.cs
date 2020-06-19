namespace libudev.Rules.Names
{
    /// <summary>
    /// Match a kernel parameter value or the value that should be written to kernel parameter.
    /// </summary>
    public class SYSCTL : NamedToken
    {
        public SYSCTL(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "SYSCTL";
    }
}