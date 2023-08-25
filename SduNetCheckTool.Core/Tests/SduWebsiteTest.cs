using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;

namespace SduNetCheckTool.Core.Tests
{
    public class SduWebsiteTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var sduWebistes = new Dictionary<string, string>();
            sduWebistes.Add("山大主页", "https://www.sdu.edu.cn");
            sduWebistes.Add("山大镜像站", "https://mirrors.sdu.edu.cn/");

            var retList = new List<string>();
            var result = TestResult.Success;

            foreach (var sduWebiste in sduWebistes)
            {
                var response = HttpUtil.GetHttpResponse(sduWebiste.Value);
                if (response != null)
                {
                    retList.Add($"{sduWebiste.Key} - {response.StatusCode}");
                    continue;
                }
                retList.Add($"{sduWebiste.Key} - 无法访问");
                result = TestResult.Failed;
            }

            return Tuple.Create<TestResult, string, IRepair>(result, string.Join("\n", retList), null);
        }
    }
}
