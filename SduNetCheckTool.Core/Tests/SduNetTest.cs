using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SduNetCheckTool.Core.Tests
{
    public class SduNetTest : ITest
    {
        public Tuple<TestResult, string> Test()
        {
            var data = new List<string>();
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = null,
                UseProxy = false
            };// 校园网判断请求禁用代理
            var client = new HttpClient(httpClientHandler);
            string text;
            try
            {
                text = client.GetStringAsync("http://101.76.193.1/cgi-bin/rad_user_info").Result;
                //text = await client.GetStringAsync("http://[2001:250:5800:11::1]/cgi-bin/rad_user_info"); //Ipv6认证地址未知
            }
            catch (Exception)
            {
                data.Add("校园网状态 : 请求失败");
                return new Tuple<TestResult, string>(TestResult.Failed,string.Join("\n",data));
            }

            if (text.Contains("not_online_error"))
            {
                data.Add("校园网状态 : 离线");
                return new Tuple<TestResult, string>(TestResult.Failed, string.Join("\n", data));
            }
            var infos = text.Split(',');
            data.Add("校园网状态 : 在线");
            data.Add($"校园网用户 : {infos[0]}");
            data.Add($"校园网IP地址 : {infos[8]}");
            return new Tuple<TestResult, string>(TestResult.Success, string.Join("\n", data));
        }
    }
}
