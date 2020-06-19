using System.Collections.Generic;
using libudev.Rules;
using libudev.Rules.Names;

namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static Rule CreateRule(string subsystem, int idVendor, int idProduct, string mode, string group, bool overrideLibinput = false)
        {
            var tokens = new List<Token>
            {
                new Token("SUBSYSTEM", Operator.Equal, subsystem),
                new Token("ACTION", Operator.Equal, "add|change"),
                new ATTR("idVendor", Operator.Equal, idVendor.ToHexFormat()),
                new ATTR("idProduct", Operator.Equal, idProduct.ToHexFormat()),
                new Token("MODE", Operator.Assign, mode),
                new Token("GROUP", Operator.Assign, group)
            };
            if (overrideLibinput)
            {
                tokens.AddRange(new Token[]
                {
                    new ENV("ID_INPUT", Operator.Assign, ""),
                    // new ENV("LIBINPUT_IGNORE_DEVICE", Operator.Assign, "1")
                });
            }
            return new Rule(tokens);
        }
    }
}