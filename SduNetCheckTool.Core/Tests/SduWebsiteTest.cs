using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Tests
{
    public class SduWebsiteTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var sduWebsites = new Dictionary<string, string>
            {
                { "山大主页", "https://www.sdu.edu.cn" },
                { "山大镜像站", "https://mirrors.sdu.edu.cn/archlinux/" }
            };

            var retList = new List<string>();
            var result = TestResult.Success;

            foreach (var sduWebsite in sduWebsites)
            {
                var domain = new Uri(sduWebsite.Value).Host;
                var ping = new Ping();
                var reply = ping.Send(domain, 2000);

                var response = HttpUtil.GetHttpResponse(sduWebsite.Value);

                if (reply.Status == IPStatus.Success && response != null)
                {
                    retList.Add($"{sduWebsite.Key} ( {sduWebsite.Value} ) - {response.StatusCode}  {reply.Address}  {reply.RoundtripTime} ms");
                    continue;
                }
                retList.Add($"{sduWebsite.Key} ( {sduWebsite.Value} ) - 无法访问");
                result = TestResult.Failed;
            }

            return Tuple.Create<TestResult, string, IRepair>(result, string.Join("\n", retList), null);
        }
    }
}
