using System;
using System.Text;
using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Utils;

namespace SduNetCheckTool.Core.Tests;

public class SduNetTest : ITest
{
    private bool _isSuccess;

    public Tuple<TestResult, string, IRepair> Test()
    {
        var strBuilder = new StringBuilder();
        _isSuccess = true;

        strBuilder.AppendLine("IPv4 认证：");
        strBuilder.Append(GetSduNetAuthInfo("http://101.76.193.1/cgi-bin/rad_user_info"));

        strBuilder.AppendLine();

        strBuilder.AppendLine("IPv6 认证：");
        strBuilder.Append(GetSduNetAuthInfo("http://[2001:250:5800:11::1]/cgi-bin/rad_user_info"));

        return new Tuple<TestResult, string, IRepair>(
            _isSuccess ? TestResult.Success : TestResult.Failed,
            strBuilder.ToString(),
            null
        );
    }

    private string GetSduNetAuthInfo(string url)
    {
        try
        {
            var response = HttpUtil.GetHttpResponse(url);
            response.EnsureSuccessStatusCode();
            var text = response.Content.ReadAsStringAsync().Result;

            if (text.Contains("not_online_error"))
            {
                _isSuccess = false;
                return "校园网状态 : 离线";
            }

            var strBuilder = new StringBuilder();
            var infos = text.Split(',');

            strBuilder.AppendLine("校园网状态 : 在线");
            strBuilder.AppendLine($"校园网用户 : {infos[0]}");
            strBuilder.AppendLine($"校园网IP地址 : {infos[8]}");

            return strBuilder.ToString();
        }
        catch (Exception)
        {
            _isSuccess = false;
            return "校园网状态 : 请求失败";
        }
    }
}