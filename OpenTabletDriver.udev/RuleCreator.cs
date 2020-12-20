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
        private static IEnumerable<DeviceIdentifier> GetDistinctIdentifiers(TabletConfiguration config)
        {
            var allIdentifiers = config.DigitizerIdentifiers.Concat(config.AuxilaryDeviceIdentifiers);
            return allIdentifiers.Distinct(new IdentifierComparer());
        }

        public static Rule CreateAccessRule(string module, string subsystem)
        {
            return new Rule(
                new Token("KERNEL", Operator.Equal, module),
                new Token("SUBSYSTEM", Operator.Equal, subsystem),
                new Token("TAG", Operator.Add, "uaccess"),
                new Token("OPTIONS", Operator.Add, $"static_node={module}")
            );
        }

        public static IEnumerable<Rule> CreateAccessRules(TabletConfiguration tablet, string subsystem)
        {
            foreach (var id in GetDistinctIdentifiers(tablet))
            {
                yield return new Rule(
                    new Token("SUBSYSTEM", Operator.Equal, subsystem),
                    new ATTRS("idVendor", Operator.Equal, id.VendorID.ToHexFormat()),
                    new ATTRS("idProduct", Operator.Equal, id.ProductID.ToHexFormat()),
                    new Token("TAG", Operator.Add, "uaccess")
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

        public static IEnumerable<Rule> CreateModuleRules(TabletConfiguration tablet)
        {
            foreach (var id in GetDistinctIdentifiers(tablet))
            {
                var module = id.VendorID == 1386 ? "wacom" : "hid_uclogic";
                yield return new Rule(
                    new Token("ACTION", Operator.Assign, "add"),
                    new Token("SUBSYSTEM", Operator.Equal, "input"),
                    new ATTRS("idVendor", Operator.Equal, id.VendorID.ToHexFormat()),
                    new ATTRS("idProduct", Operator.Equal, id.ProductID.ToHexFormat()),
                    new Token("PROGRAM", Operator.Assign, $"/sbin/rmmod {module}")
                );
            }
        }
    }
}