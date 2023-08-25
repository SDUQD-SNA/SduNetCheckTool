using SduNetCheckTool.Core.Repairs;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Tests
{
    public class NetworkAdapterTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var retList = new List<string>();
            var hasNetConnection = false;

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var ipProperties = networkInterface.GetIPProperties();
                    var dnsAddresses = ipProperties.DnsAddresses;

                    retList.Add($"网络名称: {networkInterface.Name}");
                    retList.Add($"网卡描述: {networkInterface.Description}");
                    retList.Add($"MAC地址: {networkInterface.GetPhysicalAddress()}");
                    retList.Add($"网卡类型: {networkInterface.NetworkInterfaceType}");
                    retList.Add($"网卡速度: {(networkInterface.Speed / 1000 / 1000)} Mbps");
                    retList.Add($"网络连接状态: {networkInterface.OperationalStatus}");
                    retList.Add($"DNS服务器地址: {string.Join(" ", dnsAddresses)}");
                    retList.Add("————————————————————————————");
                    if (!hasNetConnection && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                        hasNetConnection = true;
                }
            }

            if (hasNetConnection)
                return Tuple.Create<TestResult, string, IRepair>(TestResult.Success, string.Join("\n", retList), null);

            retList.Insert(0, "无网络连接！");
            return Tuple.Create<TestResult, string, IRepair>(TestResult.Failed, string.Join("\n", retList), null);

        }
    }
}
