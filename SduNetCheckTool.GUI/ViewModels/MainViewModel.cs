using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.Tests;
using SduNetCheckTool.GUI.Common;

namespace SduNetCheckTool.GUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            InitTasks();
            StartCommand = new RelayCommand(StartDetect);
        }

        private void InitTasks()
        {
            Tasks = new ObservableCollection<DetectionTask>()
            {
                new DetectionTask(new SduNetTest(),"校园网状态检测"),
                new DetectionTask(new SystemProxyTest(),"系统代理检测"),
                new DetectionTask(new SystemGatewayTest(),"系统网关检测"),
                new DetectionTask(new InternetTest(),"指定IP检测")
            };
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

        public ICommand StartCommand { get; }

        private async void StartDetect()
        {
            await Task.Run(() =>
            {
                foreach (var detectionTask in Tasks)
                {
                    detectionTask.RunTask();
                }
            });

        }
    }
}
