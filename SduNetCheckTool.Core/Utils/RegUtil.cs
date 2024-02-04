using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace SduNetCheckTool.Core.Utils
{
#nullable enable
    public static class RegUtil
    {
        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        /// <param name="def">如果为空默认返回</param>
        /// <returns>值</returns>
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

        public static void RegWriteValue(string path, string name, object value)
        {
            RegistryKey? regKey = null;
            try
            {
                regKey = Registry.CurrentUser.CreateSubKey(path);
                if (IsNullOrEmpty(value.ToString()))
                {
                    regKey?.DeleteValue(name, false);
                }
                else
                {
                    regKey?.SetValue(name, value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                regKey?.Close();
            }
        }

        public static bool IsNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            return text == "null";
        }

        public static bool IsExisted(string path, string name)
        {
            RegistryKey? regKey = null;
            var isExisted = false;
            try
            {
                regKey = Registry.CurrentUser.OpenSubKey(path);
                string[] subkeyNames = regKey.GetValueNames();
                foreach (string keyname in subkeyNames)
                {
                    if (keyname == name)
                    {
                        isExisted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                regKey?.Close();
            }
            return isExisted;
        }
    }
}
