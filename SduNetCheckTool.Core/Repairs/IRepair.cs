using System;

namespace SduNetCheckTool.Core.Repairs
{
    public interface IRepair
    {
        /// <summary>
        /// 修复函数 需要实现
        /// </summary>
        /// <returns>测试结果 成功返回附加信息/空 失败返回失败原因</returns>
        Tuple<RepairResult, string> Repair();
    }
}