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
            var result = TestResult.Success;

            try
            {
                // 读取全局代理是否使用
                var commonProxyEnabled = ProxySetting.UsedProxy();
                data.Add($"系统全局代理状态: {(commonProxyEnabled ? "开启":"关闭")}");

                // 读取pac代理是否使用
                var pacProxyEnabled = RegUtil.IsExisted(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "AutoConfigURL");
                data.Add($"PAC代理状态: {(pacProxyEnabled?"开启":"关闭")}");

                if( commonProxyEnabled || pacProxyEnabled)
                    result = TestResult.Failed;
            }
            catch (Exception)
            {
                //ignored
            }
            return new Tuple<TestResult, string, IRepair>(result, string.Join("\n", data), new ProxyRepair());
        }
    }
}
