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

namespace SduNetCheckTool.GUI.ViewModels
{
    public class TestViewModel : ObservableObject
    {
        public TestViewModel()
        {
            Init();
            StartCommand = new RelayCommand(StartDetect);
            RepairCommand = new RelayCommand(Repair);
        }

        private void Init()
        {
            Tasks = new ObservableCollection<DetectionTask>()
            {
                new DetectionTask(new NetworkAdapterTest(),"网卡检测"),
                new DetectionTask(new SduNetTest(),"校园网状态检测"),
                new DetectionTask(new SystemProxyTest(),"系统代理检测"),
                new DetectionTask(new SystemGatewayTest(),"系统网关检测"),
                new DetectionTask(new SduWebsiteTest(),"山大网站连通性检测")
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
    }
}
