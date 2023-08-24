using SduNetCheckTool.Core.Repairs;
using System;
using System.Collections.Generic;
using System.Management;

namespace SduNetCheckTool.Core.Tests
{
    public class NetworkAdapterTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var query = "SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2";
            var retList = new List<string>();
            var searcher = new ManagementObjectSearcher(query);
            var hasNetConnection = false;
            foreach (var obj in searcher.Get())
            {
                retList.Add($"网卡名称: {obj["Name"]}");
                retList.Add($"MAC地址: {obj["MACAddress"]}");
                retList.Add($"网卡类型: {obj["AdapterType"]}");
                retList.Add($"网卡速度: {(int.Parse(obj["Speed"].ToString()) / 1000 / 1000)} Mbps");
                retList.Add($"是否是物理网卡: {obj["PhysicalAdapter"]}");
                retList.Add($"制造商: {obj["Manufacturer"]}");
                retList.Add($"网络连接状态: {obj["NetConnectionStatus"]}");
                retList.Add($"网卡是否启用: {obj["NetEnabled"]}");
                retList.Add("————————————————————————————");
                if (!hasNetConnection && obj["NetEnabled"].ToString() == "True")
                    hasNetConnection = true;
            }

            if (hasNetConnection)
                return Tuple.Create<TestResult, string, IRepair>(TestResult.Success, string.Join("\n", retList), null);

            retList.Insert(0, "无网络连接！");
            return Tuple.Create<TestResult, string, IRepair>(TestResult.Failed, string.Join("\n", retList), null);

        }
    }
}
