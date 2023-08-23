using SduNetCheckTool.Core.Tests;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.Core.CustomInputTest
{
    public class InternetTest : ICustomInputTest
    {
        public string Test(string input)
        {
            var data = new List<string>();
            try
            {
                var ping = new Ping();
                var testSite = input; // string testSite = ClientInput();

                data.Add($"您要检测的网页为: {testSite}");
                var reply = ping.Send(testSite);
                if (reply.Status == IPStatus.Success)
                {
                    data.Add($"您所检测网页对应的ip地址为: {reply.Address}");
                    data.Add($"时间:{reply.RoundtripTime} ms");
                    data.Add($"TTL: {reply.Options.Ttl}");
                }
                else
                {
                    data.Add($"未能ping通!");
                }
            }
            catch (Exception)
            {
                //ignored
            }
            return string.Join("\n", data);
        }
    }
}
