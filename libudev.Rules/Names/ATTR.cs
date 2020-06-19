namespace libudev.Rules.Names
{
    /// <summary>
    /// Match sysfs attribute values of the event device, or the value that should be written to a sysfs attribute of the event device.
    /// </summary>
    public class ATTR : NamedToken
    {
        public ATTR(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "ATTR";
    }
}