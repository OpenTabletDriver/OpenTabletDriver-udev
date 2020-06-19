using System.Collections.Generic;
using libudev.Rules;
using libudev.Rules.Names;
using TabletDriverPlugin.Tablet;

namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static Rule CreateAccessRule(TabletProperties tablet, string mode)
        {
            return new Rule(new Token[]
            {
                new Token("SUBSYSTEM", Operator.Equal, "hidraw"),
                new ATTRS("idVendor", Operator.Equal, tablet.VendorID.ToHexFormat()),
                new ATTRS("idProduct", Operator.Equal, tablet.ProductID.ToHexFormat()),
                new Token("MODE", Operator.Assign, mode),
            });
        }

        public static Rule CreateOverrideRule(TabletProperties tablet)
        {
            return new Rule(new Token[]
            {
                new Token("SUBSYSTEM", Operator.Equal, "input"),
                new ATTRS("idVendor", Operator.Equal, tablet.VendorID.ToHexFormat()),
                new ATTRS("idProduct", Operator.Equal, tablet.ProductID.ToHexFormat()),
                new ENV("LIBINPUT_IGNORE_DEVICE", Operator.Assign, "1")
            });
        }
    }
}