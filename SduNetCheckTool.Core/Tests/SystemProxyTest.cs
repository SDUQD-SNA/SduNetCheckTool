using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;
using SduNetCheckTool.Core.Repairs;

namespace SduNetCheckTool.Core.Tests
{
    public class SystemProxyTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var data = new List<string>();
            var result = TestResult.Failed;
            var result1 = TestResult.Failed;
            var result2 = TestResult.Failed;

            try
            {
                var proxyEnabled = RegUtil.RegReadValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", "-1");
                string proxyEnabledString;
                switch (proxyEnabled)
                {
                    case "1":
                        proxyEnabledString = "开启";
                        break;
                    case "0":
                        proxyEnabledString = "关闭";
                        result1 = TestResult.Success;
                        break;
                    case "-1":
                        proxyEnabledString = "获取失败";
                        break;
                    default:
                        proxyEnabledString = "Unknown";
                        break;
                }

                data.Add($"系统全局代理状态:{proxyEnabledString}");

                var PACproxyEnabledString = (RegUtil.IsExisted(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "AutoConfigURL") ? "开启" : "关闭");
                if (PACproxyEnabledString == "关闭") result2 = TestResult.Success;

                data.Add($"PAC代理状态:{PACproxyEnabledString}");

                result = (result1 == TestResult.Success) && (result2 == TestResult.Success) ? TestResult.Success : TestResult.Failed;
            }
            catch (Exception)
            {
                //ignored
            }
            return new Tuple<TestResult, string, IRepair>(result, string.Join("\n", data), new ProxyRepair());
        }
    }
}
