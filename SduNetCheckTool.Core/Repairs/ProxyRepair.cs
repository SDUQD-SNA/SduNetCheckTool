using SduNetCheckTool.Core.Utils;
using System;

namespace SduNetCheckTool.Core.Repairs
{
    public class ProxyRepair:IRepair
    {
        public Tuple<RepairResult, string> Repair()
        {
            RegUtil.RegWriteValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", "0");
            return new Tuple<RepairResult, string>(RepairResult.Success, "已经尝试关闭系统代理");
        }
    }
}
