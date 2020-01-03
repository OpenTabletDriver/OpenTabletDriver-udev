using System;
using System.Linq;

namespace OpenTabletDriver.udev
{
    internal static class Extensions
    {
        public static string ToHexFormat(this int value)
        {
            var bytes = BitConverter.GetBytes(value)[0..2];
            var raw = BitConverter.ToString(BitConverter.IsLittleEndian ? bytes.Reverse().ToArray() : bytes);
            return raw.Replace("-", "").ToLower();
        }
    }
}