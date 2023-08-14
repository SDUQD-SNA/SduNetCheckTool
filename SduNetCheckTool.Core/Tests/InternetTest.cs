using SduNetCheckTool.Core.Utils;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.Tests
{
    public class InternetTest : ITest
    {
        public Tuple<TestResult, string> Test()
        {
            var data = new List<string>();
            var result = TestResult.Failed;
            try
            {
                Ping ping = new Ping();
                string testSite = "www.google.com"; // string testSite = ClientInput();

                data.Add($"您要检测的网页为: {testSite}");
                PingReply reply = ping.Send(testSite);
                if (reply.Status == IPStatus.Success)
                {
                    data.Add($"您所检测网页对应的ip地址为: {reply.Address}");
                    data.Add($"时间:{reply.RoundtripTime} ms");
                    data.Add($"TTL: {reply.Options.Ttl}");
                    result = TestResult.Success;
                }
                else
                {
                    data.Add($"未能ping通!");
                    result = TestResult.Failed;
                }
            }
            catch (Exception)
            {
                //ignored
            }
            return new Tuple<TestResult, string>(result,string.Join("\n",data));
        }
    }
}
