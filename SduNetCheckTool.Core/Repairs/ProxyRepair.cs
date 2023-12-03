using SduNetCheckTool.Core.Utils;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SduNetCheckTool.Core.Repairs
{
    public class ProxyRepair : IRepair
    {
        public Tuple<RepairResult, string> Repair()
        {
            try
            {
                RegUtil.RegWriteValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", "0");
                RegUtil.RegWriteValue(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", "AutoConfigURL", "");
                ResetIEProxy();
            }
            catch (Exception ex)
            {
                return new Tuple<RepairResult, string>(RepairResult.Failed, ex.Message);
            }
            return new Tuple<RepairResult, string>(RepairResult.Success, "已经尝试关闭系统全局代理和PAC代理，此操作并不会关闭你的代理软件！");
        }

        private enum RET_ERRORS : int
        {
            RET_NO_ERROR = 0,
            INVALID_FORMAT = 1,
            NO_PERMISSION = 2,
            SYSCALL_FAILED = 3,
            NO_MEMORY = 4,
            INVAILD_OPTION_COUNT = 5,
        };

        public static void ResetIEProxy()
        {
            ExecuteSysproxy("set 1 - - -");
        }

        private static void ExecuteSysproxy(string arguments)
        {
            // using event to avoid hanging when redirect standard output/error
            // ref: https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
            // and http://blog.csdn.net/zhangweixing0/article/details/7356841
            using AutoResetEvent outputWaitHandle = new(false);
            using AutoResetEvent errorWaitHandle = new(false);
            using Process process = new();

            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "sysproxy.exe";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            // Need to provide encoding info, or output/error strings we got will be wrong.
            process.StartInfo.StandardOutputEncoding = Encoding.Unicode;
            process.StartInfo.StandardErrorEncoding = Encoding.Unicode;

            process.StartInfo.CreateNoWindow = true;

            StringBuilder output = new(1024);
            StringBuilder error = new(1024);

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data == null)
                {
                    outputWaitHandle.Set();
                }
                else
                {
                    output.AppendLine(e.Data);
                }
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data == null)
                {
                    errorWaitHandle.Set();
                }
                else
                {
                    error.AppendLine(e.Data);
                }
            };
            try
            {
                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                // log the arguments
                throw new Exception(process.StartInfo.Arguments);
            }
            string stderr = error.ToString();
            string stdout = output.ToString();

            int exitCode = process.ExitCode;
            if (exitCode != (int)RET_ERRORS.RET_NO_ERROR)
            {
                throw new Exception(stderr);
            }
        }
    }
}
