using System.Collections.Generic;
using System.Linq;
using libudev.Rules;
using libudev.Rules.Names;
using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.udev.Comparers;

namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static Rule CreateAccessRule(string module, string mode)
        {
            return new Rule(
                new Token("KERNEL", Operator.Equal, module),
                new Token("MODE", Operator.Assign, mode)
            );
        }

        private static IEnumerable<DeviceIdentifier> GetDistinctIdentifiers(TabletConfiguration config)
        {
            var allIdentifiers = config.DigitizerIdentifiers.Concat(config.AuxilaryDeviceIdentifiers);
            return allIdentifiers.Distinct(new IdentifierComparer());
        }

        public static IEnumerable<Rule> CreateAccessRules(TabletConfiguration tablet, string subsystem, string mode)
        {
            foreach (var id in GetDistinctIdentifiers(tablet))
            {
                yield return new Rule(
                    new Token("SUBSYSTEM", Operator.Equal, subsystem),
                    new ATTRS("idVendor", Operator.Equal, id.VendorID.ToHexFormat()),
                    new ATTRS("idProduct", Operator.Equal, id.ProductID.ToHexFormat()),
                    new Token("MODE", Operator.Assign, mode)
                );
            }
        }

        public static IEnumerable<Rule> CreateOverrideRules(TabletConfiguration tablet)
        {
            foreach (var id in GetDistinctIdentifiers(tablet))
            {
                yield return new Rule(
                    new Token("SUBSYSTEM", Operator.Equal, "input"),
                    new ATTRS("idVendor", Operator.Equal, id.VendorID.ToHexFormat()),
                    new ATTRS("idProduct", Operator.Equal, id.ProductID.ToHexFormat()),
                    new ENV("LIBINPUT_IGNORE_DEVICE", Operator.Assign, "1")
                );
            }
        }
    }
}