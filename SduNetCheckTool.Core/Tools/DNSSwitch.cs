using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using SduNetCheckTool.Core.Utils;

namespace SduNetCheckTool.Core.Tools
{
    public static class DNSSwitch
    {
        private static readonly string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        private static readonly Regex regex = new(pattern);
        public static bool IsValidIPv4(string ip) => regex.IsMatch(ip);

        public static string Switch(NetworkInterface[] netInterfaces, string dnsServer)
        {
            var data = new StringBuilder();

            data.AppendLine("脚本执行结果：");

            foreach (var netInterface in netInterfaces)
            {
                // 修改 DNS 为 dnsServer or DHCP
                if (dnsServer == "DHCP")
                {
                    NetworkInterfaceHelper.SetDnsServers(netInterface, []);
                    data.AppendLine($"已将 {netInterface.Name} 的 DNS 设置为 DHCP");
                }
                else
                {
                    NetworkInterfaceHelper.SetDnsServers(netInterface, [dnsServer]);
                    data.AppendLine($"已将 {netInterface.Name} 的 DNS 设置为 {dnsServer}");
                }
            }

            data.AppendLine("结束");

            return data.ToString();
        }
    }
}
