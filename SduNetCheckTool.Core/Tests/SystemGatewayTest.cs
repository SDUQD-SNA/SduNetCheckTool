using SduNetCheckTool.Core.Repairs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Tests
{
    public class SystemGatewayTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var data = new List<string>();
            var result = TestResult.Failed;
            try
            {
                Ping ping = new Ping();

                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in interfaces)
                {
                    IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
                    GatewayIPAddressInformationCollection gatewayIPAddressInformation = iPInterfaceProperties.GatewayAddresses;
                    IPAddressCollection addresses = iPInterfaceProperties.DhcpServerAddresses;
                    foreach (var item in gatewayIPAddressInformation)
                    {
                        data.Add($"网卡信息:......{networkInterface.Description}");
                        data.Add($"网关地址:......{item.Address}");
                        PingReply reply = ping.Send(item.Address);
                        data.Add($"是否封禁:......{(reply.Status == IPStatus.Success ? "未封禁" : "封禁")}");
                        if (reply.Status == IPStatus.Success)
                        {
                            data.Add($"网关延迟:......{reply.RoundtripTime} ms");
                        }
                        if (addresses.Count > 0)
                        {
                            foreach (IPAddress address in addresses)
                            {
                                data.Add($"Dhcp地址:.....{address}\n");
                            }
                        }
                    }
                }
                result = TestResult.Success;
            }
            catch (Exception)
            {
                //ignored
            }
            return new Tuple<TestResult, string, IRepair>(result,string.Join("\n",data), null);
        }
    }
}
