using SduNetCheckTool.Core.Utils;
using System;

namespace SduNetCheckTool.Core.Repairs;

public class ProxyRepair : IRepair
{
    public Tuple<RepairResult, string> Repair()
    {
        try
        {
            RegUtil.RegWriteValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable",
                "0");
            RegUtil.RegWriteValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "AutoConfigURL",
                "");
            ProxySetting.UnsetProxy();
        }
        catch (Exception ex)
        {
            return new Tuple<RepairResult, string>(RepairResult.Failed, ex.Message);
        }

        return new Tuple<RepairResult, string>(RepairResult.Success, "已经尝试关闭系统全局代理和PAC代理，此操作并不会关闭你的代理软件！");
    }
}