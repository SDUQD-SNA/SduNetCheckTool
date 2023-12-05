using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Tests;
using SduNetCheckTool.GUI.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SduNetCheckTool.GUI.Utils;
using Microsoft.Toolkit.Uwp.Notifications;

namespace SduNetCheckTool.GUI.ViewModels
{
    public class TestViewModel : ObservableObject
    {
        public TestViewModel()
        {
            Init();
            StartCommand = new RelayCommand(StartDetect);
            RepairCommand = new RelayCommand(Repair);
            ExportReportCommand = new RelayCommand(ExportReport);
        }

        private void Init()
        {
            Tasks = new ObservableCollection<DetectionTask>()
            {
                new DetectionTask(new NetworkAdapterTest(),"网卡检测"),
                new DetectionTask(new SduNetTest(),"校园网状态检测"),
                new DetectionTask(new SystemProxyTest(),"系统代理检测"),
                new DetectionTask(new SystemGatewayTest(),"系统网关检测"),
                new DetectionTask(new SduWebsiteTest(),"山大网站连通性检测"),
                new DetectionTask(new CommonWebsiteTest(),"常用网站检测")
            };
            _repairs = new Collection<IRepair>();
        }

        /// <summary>
        /// 任务
        /// </summary>
        private ObservableCollection<DetectionTask> _tasks;

        public ObservableCollection<DetectionTask> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        private Collection<IRepair> _repairs;

        public ICommand StartCommand { get; }

        public ICommand RepairCommand { get; }

        public ICommand ExportReportCommand { get; }

        private async void Repair()
        {
            if (_repairs == null || _repairs.Count == 0)
                return;
            await Task.Run(() =>
            {
                var output = new List<string>();
                foreach (var repair in _repairs)
                {
                    var result = repair.Repair();
                    output.Add(result.Item1 == RepairResult.Success ? "修复成功" : "修复失败");
                    output.Add(result.Item2);
                    output.Add("\n");
                }

                MessageBox.Show(string.Join("\n", output));
            });
        }


        private async void StartDetect()
        {
            await Task.Run(() =>
            {
                _repairs.Clear();
                foreach (var detectionTask in Tasks)
                {
                    var result = detectionTask.RunTask();
                    if (result != null)
                        _repairs.Add(result);
                }
            });

        }

        private void ExportReport()
        {
            var output = FileUtil.ExportReport(Tasks);
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
            }
        }
    }
}
