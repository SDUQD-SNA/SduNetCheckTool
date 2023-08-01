using Microsoft.Win32;
using System;

namespace SduNetCheckTool.Core.Utils
{
    public static class RegUtil
    {
        public static string RegReadValue(string path, string name, string def)
        {
            RegistryKey regKey = null;
            try
            {
                regKey = Registry.CurrentUser.OpenSubKey(path, false);
                var value = regKey?.GetValue(name).ToString();
                return IsNullOrEmpty(value) ? def : value;
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                regKey?.Close();
            }
            return def;
        }


        public static bool IsNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            return text == "null";
        }
    }
}
