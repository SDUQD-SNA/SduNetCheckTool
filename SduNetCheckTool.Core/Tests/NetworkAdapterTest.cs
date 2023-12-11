using SduNetCheckTool.Core.Repairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Tests
{
    public class NetworkAdapterTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var retList = new List<string>();
            var hasNetConnection = false;

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();

                    var test = networkInterface.GetIPProperties();
     
                    string macInfo = networkInterface.GetPhysicalAddress().ToString() != "" ? networkInterface.GetPhysicalAddress().ToString() : "--";
                    string connectionSpeed = networkInterface.OperationalStatus.ToString() == "Up" ? (networkInterface.Speed / 1000 / 1000).ToString() + " Mbps" : "--";
                    string dhcpServers = iPInterfaceProperties.DhcpServerAddresses.Count() > 0 ? string.Join("  ", iPInterfaceProperties.DhcpServerAddresses) : "--";

                    retList.Add($"网络名称:  {networkInterface.Name}");
                    retList.Add($"网卡描述:  {networkInterface.Description}");
                    retList.Add($"MAC地址:  {macInfo}");
                //    retList.Add($"网卡类型:  {networkInterface.NetworkInterfaceType}");
                    retList.Add($"连接状态:  {networkInterface.OperationalStatus}");
                    retList.Add($"连接速度:  {connectionSpeed}");
                    retList.Add($"DNS服务器:  {string.Join("  ", iPInterfaceProperties.DnsAddresses)}");
                    retList.Add($"DHCP服务器:  {dhcpServers}");

                    foreach(var item in iPInterfaceProperties.UnicastAddresses)
                    {
                        retList.Add($"IP地址:  {item.Address}");
                    }

                    foreach (var item in iPInterfaceProperties.GatewayAddresses)
                    {
                        retList.Add($"网关地址:  {item.Address}");

                        Ping ping = new Ping();
                        PingReply reply = ping.Send(item.Address, 200);

                        string delay = reply.Status == IPStatus.Success ? reply.RoundtripTime.ToString() + " ms" : "--";
                        retList.Add($"网关延迟:  {delay}");
                    }

                    retList.Add("");

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
