namespace libudev.Rules.Names
{
    /// <summary>
    /// Add a program to the list of programs to be executed after processing all the rules for a specific event, depending on <see cref="Token.Key"/>.\
    /// Supported keys: `program`, `builtin`
    /// </summary>
    public class RUN : NamedToken
    {
        public RUN(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "RUN";
    }
}