using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SduNetCheckTool.Core.Repairs
{
    public class DhcpRepair : IRepair
    {
        public Tuple<RepairResult, string> Repair()
        {
            var data = new List<string>();

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "ipconfig /release && ipconfig /renew";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            data.Add("脚本执行结果：");
            data.Add(output);


            return new Tuple<RepairResult, string>(RepairResult.Success, string.Join("\n", data));
        }
    }
}
