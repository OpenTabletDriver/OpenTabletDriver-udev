namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static string CreateRule(string subsystem, int idVendor, int idProduct, string mode, string group)
        {
            var parts = new string[]
            {
                CreateFullProperty("SUBSYSTEM", subsystem),
                CreateAttrProperty("idVendor", idVendor.ToHexFormat()),
                CreateAttrProperty("idProduct", idProduct.ToHexFormat()),
                CreateShortProperty("MODE", mode),
                CreateShortProperty("GROUP", group)
            };
            return string.Join(", ", parts);
        }

        static string CreateAttrProperty(string index, string value)
        {
            return CreateFullProperty($@"ATTRS{{{index}}}", value);
        }

        static string CreateFullProperty(string group, string value)
        {
            return string.Format("{0}==\"{1}\"", group, value);
        }

        static string CreateShortProperty(string group, string value)
        {
            return string.Format("{0}=\"{1}\"", group, value);
        }
    }
}