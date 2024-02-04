using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Tests;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.ViewModels
{
    public partial class TestViewModel : ObservableObject
    {
        public TestViewModel()
        {
            Init();
        }

        private void Init()
        {
            Tasks =
            [
                new DetectionTask(new NetworkAdapterTest(), "网卡检测"),
                new DetectionTask(new SduNetTest(), "校园网状态检测"),
                new DetectionTask(new SystemProxyTest(), "系统代理检测"),
                new DetectionTask(new SduWebsiteTest(), "山大网站连通性检测"),
                new DetectionTask(new CommonWebsiteTest(), "常用网站检测")
            ];
            Repairs = [];
        }

        /// <summary>
        /// 任务
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<DetectionTask> _tasks;

        [ObservableProperty]
        private Collection<IRepair> _repairs;

        [RelayCommand]
        private Task Repair()
        {
            return Task.Run(() =>
            {
                if (Repairs.Count == 0)
                    return;

                var output = new List<string>();
                foreach (var repair in Repairs)
                {
                    var result = repair.Repair();
                    output.Add(result.Item1 == RepairResult.Success ? "修复成功" : "修复失败");
                    output.Add(result.Item2);
                    output.Add("\n");
                }

                //MessageBox.Show(string.Join("\n", output));
            });
        }

        [RelayCommand]
        private Task StartDetect()
        {
            return Task.Run(() =>
            {
                Repairs.Clear();
                foreach (var detectionTask in Tasks)
                {
                    var result = detectionTask.RunTask();
                    if (result != null)
                        Repairs.Add(result);
                }
            });
        }

        [RelayCommand]
        private void ExportReport()
        {
            /*var output = FileUtil.ExportReport(Tasks);
            if (output == "NoRecords")
            {
                MessageBox.Show("请先点击'开始检测'运行测试! >_<", "提示");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("日志文件已生成! (⑅•ᴗ•⑅) \n路径: " + output + "\n点击'确定'打开日志并自动复制到剪贴板!", "提示");
                if (result == MessageBoxResult.OK)
                {
                    System.Diagnostics.Process.Start(output);
                    string data = FileUtil.ReadFile(output);
                    if (data == "FileNotExists" || data == "-1")
                    {
                        MessageBox.Show("出现了一点小错误...");
                        return;
                    }
                    Clipboard.SetText(data);
                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("已经成功复制内容到剪贴板啦! ๐•ᴗ•๐")
                        .AddText("可以直接分享给同学哦~")
                        .Show();
                }
            }*/
        }

        [RelayCommand]
        private Task SingleReRun(DetectionTask task)
        {
            return Task.Run(() =>
            {
                task.RunTask();
            });
        }
    }
}
