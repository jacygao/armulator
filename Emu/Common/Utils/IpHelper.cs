using System.Net;

namespace Emu.Common.Utils
{
    public class IpHelper
    {
        private static readonly Random _random = new();

        public static string GenerateRandomIPv4()
        {
            return $"{_random.Next(1, 256)}.{_random.Next(0, 256)}.{_random.Next(0, 256)}.{_random.Next(1, 256)}";
        }

        public static bool IsValidIPv4(string ipString)
        {
            if (IPAddress.TryParse(ipString, out IPAddress ipAddress))
            {
                return ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
            }
            return false;
        }
    }
}
