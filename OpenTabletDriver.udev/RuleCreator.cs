using System.Collections.Generic;

namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static string CreateRule(string subsystem, int idVendor, int idProduct, string mode, string group, bool overrideLibinput = false)
        {
            var rules = new List<string>
            {
                Match("SUBSYSTEM", subsystem),
                MatchATTRS("idVendor", idVendor.ToHexFormat()),
                MatchATTRS("idProduct", idProduct.ToHexFormat()),
                Action("MODE", mode),
                Action("GROUP", group),
            };
            if (overrideLibinput)
            {
                rules.AddRange(new string[]
                {
                    MatchENV("ID_VENDOR_ID", idVendor.ToHexFormat()),
                    MatchENV("ID_MODEL_ID", idProduct.ToHexFormat()),
                    ActionENV("LIBINPUT_IGNORE_DEVICE", "1")
                });
            }
            return string.Join(", ", rules);
        }

        private static string Match(string key, string value)
        {
            return $"{key}==\"{value}\"";
        }

        private static string Action(string key, string value)
        {
            return $"{key}=\"{value}\"";
        }

        private static string NamedMatch(string name, string key, string value)
        {
            return Match($"{name}{{{key}}}", value);
        }

        private static string NamedAction(string name, string key, string value)
        {
            return Action($"{name}{{{key}}}", value);
        }

        private static string MatchENV(string key, string value)
        {
            return NamedMatch("ENV", key, value);
        }

        private static string ActionENV(string key, string value)
        {
            return NamedAction("ENV", key, value);
        }

        private static string MatchATTRS(string key, string value)
        {
            return NamedMatch("ATTRS", key, value);
        }
    }
}