using System.Linq;
using System.Net.NetworkInformation;
using System.Management;
using System.Text;

namespace SduNetCheckTool.Core.CustomInputTest
{
    public class DNSSwitch : ICustomInputTest
    {
        private enum SetOption { DNS, DHCP };
        private SetOption Option { get; set; }
        // default DNS server
        private readonly string dnsServer = "114.114.114.114";

        public DNSSwitch()
        {
            this.Option = SetOption.DNS; // 默认修改 DNS
        }

        public string Test(string input)
        {
            // TODO 增加一些常用的 DNS 服务器选项 放在 view 里面
            var data = new StringBuilder();

            var netInterfaces = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Tunnel);

            data.AppendLine("脚本执行结果：");

            foreach (var netInterface in netInterfaces)
            {
                // 修改 DNS 为 dnsServer or DHCP
                if (Option == SetOption.DNS)
                {
                    // set dns for net interface
                    var dnsServers = netInterface.GetIPProperties().DnsAddresses;
                    SetDnsServers(netInterface, [dnsServer]);
                    data.AppendLine($"修改 {netInterface.Name} DNS 为 {dnsServer}");
                }
                else if (Option == SetOption.DHCP)
                {
                    SetDnsServers(netInterface, []);
                    data.AppendLine($"修改 {netInterface.Name} DNS 为 DHCP");
                }
            }

            data.AppendLine("结束");

            return data.ToString();
        }

        private void SetDnsServers(NetworkInterface netInterface, string[] dnsServers)
        {
            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var managementObjects = managementClass.GetInstances();

            foreach (var managementObject in managementObjects.Cast<ManagementObject>()
                .Where(mo => (bool)mo["IPEnabled"] && mo["Description"].ToString() == netInterface.Description))
            {
                var inParams = managementObject.GetMethodParameters("SetDNSServerSearchOrder");
                inParams["DNSServerSearchOrder"] = dnsServers;
                managementObject.InvokeMethod("SetDNSServerSearchOrder", inParams, null);
            }
        }
    }
}
