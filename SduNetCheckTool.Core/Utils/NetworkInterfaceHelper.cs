using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Utils
{
    public static class NetworkInterfaceHelper
    {
        public static NetworkInterface[] GetAvailableNetworkInterfaces()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .ToArray();
        }
        public static List<(string Name, NetworkInterface Interface)> AvailableNetworkInterfacesWithName => NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .Select(n => (n.Name, n))
                .ToList();

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
    }
}
