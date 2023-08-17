using System;
using SduNetCheckTool.Core.Repairs;

namespace SduNetCheckTool.Core.Tests
{
    public interface ITest
    {
        /// <summary>
        /// 测试函数 需要实现
        /// </summary>
        /// <returns>TestResult 测试结果  string 附加提示  IRepair 修复函数</returns>
        Tuple<TestResult,string,IRepair> Test();
    }
}
