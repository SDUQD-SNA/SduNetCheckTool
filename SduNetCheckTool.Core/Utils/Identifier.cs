using System;
using System.Diagnostics;
using System.Security.Principal;

namespace SduNetCheckTool.Core.Utils
{
    public static class Identifier
    {
        /// <summary>
        /// IsAdministrator
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            try
            {
                WindowsIdentity current = WindowsIdentity.GetCurrent();
                WindowsPrincipal windowsPrincipal = new(current);
                //WindowsBuiltInRole可以枚举出很多权限，例如系统用户、User、Guest等等
                return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                // log
                return false;
            }
        }

        public static string ExePath => Process.GetCurrentProcess().MainModule.FileName ?? string.Empty;
        public static string StartupPath => AppDomain.CurrentDomain.BaseDirectory;

        public static string AppendQuotes(string value) => string.IsNullOrEmpty(value) ? string.Empty : $"\"{value}\"";


        public static void RebootAsAdmin()
        {
            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = true,
                Arguments = "rebootas",
                WorkingDirectory = StartupPath,
                FileName = AppendQuotes(ExePath),
                Verb = "runas",
            };
            try
            {
                Process.Start(startInfo);
                Exit();
            }
            catch { }
        }

        public static void Exit()
        {
            //Application.Current.Shutdown();
            Environment.Exit(0);
            
        }
    }
}
