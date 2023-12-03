using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;

namespace SduNetCheckTool.Core.Tests
{
    public class CommonWebsiteTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var sduWebsites = new Dictionary<string, string>
            {
                { "哔哩哔哩" , "https://www.bilibili.com" },
                { "知乎" , "https://www.zhihu.com" },
                { "知网" , "https://www.cnki.net" }
            };

            var retList = new List<string>();
            var result = TestResult.Success;

            foreach (var sduWebsite in sduWebsites)
            {
                var response = HttpUtil.GetHttpResponse(sduWebsite.Value);
                if (response != null)
                {
                    retList.Add($"{sduWebsite.Key} ( {sduWebsite.Value} ) - {response.StatusCode}");
                    continue;
                }
                retList.Add($"{sduWebsite.Key} ( {sduWebsite.Value} ) - 无法访问");
                result = TestResult.Failed;
            }

            return Tuple.Create<TestResult, string, IRepair>(result, string.Join("\n", retList), null);
        }
    }
}
