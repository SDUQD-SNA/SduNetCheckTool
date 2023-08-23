namespace SduNetCheckTool.Core.CustomInputTest
{
    public interface ICustomInputTest
    {
        /// <summary>
        /// 自定义输入的测试
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>返回的结果</returns>
        string Test(string input);
    }
}
