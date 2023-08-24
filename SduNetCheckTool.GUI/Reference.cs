using System.Reflection;

namespace SduNetCheckTool.GUI
{
    public static class Reference
    {
        public static string Name = "SDUNET CHECK TOOL";
        public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
