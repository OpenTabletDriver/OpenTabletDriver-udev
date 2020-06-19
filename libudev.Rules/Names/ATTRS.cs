namespace libudev.Rules.Names
{
    /// <summary>
    /// Search the devpath upwards for a device with matching sysfs attribute values.
    /// </summary>
    public class ATTRS : NamedToken
    {
        public ATTRS(string key, Operator op, string value) : base(key, op, value)
        {
        }

        public override string Name => "ATTRS";
    }
}