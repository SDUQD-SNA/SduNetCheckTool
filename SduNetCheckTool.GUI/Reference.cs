using System.Diagnostics;
using System.Reflection;

namespace SduNetCheckTool.GUI
{
    public static class Reference
    {
        public static string Name = "SDUNET CHECK TOOL";
        public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
        public static string Stable = "测试版";
#else
        public static string Stable = "稳定版";
#endif
    }
}
