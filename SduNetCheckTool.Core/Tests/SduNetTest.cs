using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;

namespace SduNetCheckTool.Core.Tests
{
    public class SduNetTest : ITest
    {
        public Tuple<TestResult, string, IRepair> Test()
        {
            var data = new List<string>();
            
            string text;
            try
            {
                var response = HttpUtil.GetHttpResponse("http://101.76.193.1/cgi-bin/rad_user_info");
                response.EnsureSuccessStatusCode();
                text = response.Content.ReadAsStringAsync().Result;
                //text = await client.GetStringAsync("http://[2001:250:5800:11::1]/cgi-bin/rad_user_info"); //Ipv6认证地址未知
            }
            catch (Exception)
            {
                data.Add("校园网状态 : 请求失败");
                return new Tuple<TestResult, string, IRepair>(TestResult.Failed,string.Join("\n",data), null);
            }

            if (text.Contains("not_online_error"))
            {
                data.Add("校园网状态 : 离线");
                return new Tuple<TestResult, string, IRepair>(TestResult.Failed, string.Join("\n", data), null);
            }
            var infos = text.Split(',');
            data.Add("校园网状态 : 在线");
            data.Add($"校园网用户 : {infos[0]}");
            data.Add($"校园网IP地址 : {infos[8]}");
            return new Tuple<TestResult, string, IRepair>(TestResult.Success, string.Join("\n", data), null);
        }
    }
}
