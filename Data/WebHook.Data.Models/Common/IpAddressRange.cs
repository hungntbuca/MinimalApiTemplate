using System;
using System.Net;
using System.Net.Sockets;

namespace WebHook.Data.Models.Common
{
    public class IPAddressRange
    {
        private readonly IPAddress _startIpAddress;
        private readonly IPAddress _endIpAddress;

        public IPAddressRange(IPAddress startIpAddress, IPAddress endIpAddress)
        {
            _startIpAddress = startIpAddress;
            _endIpAddress = endIpAddress;
        }

        public static IPAddressRange Parse(string range)
        {
            var parts = range.Split('/');

            var ipAddress = ConvertToIPv4(IPAddress.Parse(parts[0]));

            // If there is no subnet prefix, both start and end IP addresses are the same.
            if (parts.Length == 1)
                return new IPAddressRange(ipAddress, ipAddress);

            var prefixLength = int.Parse(parts[1]);
            var prefixBytes = ipAddress.GetAddressBytes();
            var bitMask = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                bitMask[i] = (byte)(i < prefixLength / 8 ? 255 : prefixLength % 8 == 0 || i * 8 >= prefixLength ? 0 : (byte)(255 << 8 - prefixLength % 8));
            }

            var startIpAddress = new IPAddress(ByteArrayAnd(prefixBytes, bitMask));
            var endIpAddress = new IPAddress(ByteArrayOr(prefixBytes, ByteArrayNot(bitMask)));

            return new IPAddressRange(startIpAddress, endIpAddress);
        }

        public bool Contains(IPAddress ipAddress)
        {
            ipAddress = ConvertToIPv4(ipAddress);

            var ipAddressBytes = ipAddress.GetAddressBytes();
            var startIpAddressBytes = _startIpAddress.GetAddressBytes();
            var endIpAddressBytes = _endIpAddress.GetAddressBytes();

            for (int i = 0; i < 4; i++)
            {
                if (ipAddressBytes[i] < startIpAddressBytes[i] || ipAddressBytes[i] > endIpAddressBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] ByteArrayAnd(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                throw new ArgumentException("Array lengths must be equal.");

            var result = new byte[array1.Length];

            for (int i = 0; i < array1.Length; i++)
            {
                result[i] = (byte)(array1[i] & array2[i]);
            }

            return result;
        }

        private static byte[] ByteArrayOr(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                throw new ArgumentException("Array lengths must be equal.");

            var result = new byte[array1.Length];

            for (int i = 0; i < array1.Length; i++)
            {
                result[i] = (byte)(array1[i] | array2[i]);
            }

            return result;
        }

        private static byte[] ByteArrayNot(byte[] array)
        {
            var result = new byte[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = (byte)~array[i];
            }

            return result;
        }

        private static IPAddress ConvertToIPv4(IPAddress ipAddress) => ipAddress.AddressFamily == AddressFamily.InterNetworkV6 ? ipAddress.MapToIPv4() : ipAddress;
    }
}
