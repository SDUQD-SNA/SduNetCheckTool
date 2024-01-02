using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Utils
{
    public static class NetworkInterfaceHelper
    {
        public static NetworkInterface[] GetAvailableNetworkInterfaces => NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .ToArray();


        public static void SetDnsServers(NetworkInterface netInterface, string[] dnsServers)
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

        public static void SetDnsServersUsingNetsh(NetworkInterface netInterface, string[] dnsServers)
        {
            string dnsServersString = string.Join(" ", dnsServers);
            string command;
            if (dnsServers.Length == 0)
            {
                command = $"interface ipv4 set dns \"{netInterface.Name}\" source=dhcp";
            }
            else
            {
                command = $"interface ipv4 set dns \"{netInterface.Name}\" static {dnsServersString}";
            }
            // netsh interface ipv4 set dns name=3 static 8.8.8.8

            Process process = new();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();

        }

    }
}
