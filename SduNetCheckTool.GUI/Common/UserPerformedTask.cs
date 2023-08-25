using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.CustomInputTest;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SduNetCheckTool.GUI.Common
{
    public class UserPerformedTask : ObservableObject
    {
        public UserPerformedTask(ICustomInputTest test, string name)
        {
            _test = test;
            Name = name;
            RunCommand = new AsyncRelayCommand<string>(RunTask);
        }

        public Task RunTask(string input)
        {
            return Task.Run(() =>
            {
                TaskStatusEnum = TaskStatusEnum.Running;
                Tips = _test.Test(input);
                TaskStatusEnum = TaskStatusEnum.Completed;
            });
        }

        public ICommand RunCommand { get; private set; }

        /// <summary>
        /// 测试类
        /// </summary>
        private readonly ICustomInputTest _test;

        /// <summary>
        /// 任务名字
        /// </summary>
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// 完成状态
        /// </summary>
        private TaskStatusEnum _taskStatusEnum = TaskStatusEnum.Waiting;

        public TaskStatusEnum TaskStatusEnum
        {
            get => _taskStatusEnum;
            set => SetProperty(ref _taskStatusEnum, value);
        }

        /// <summary>
        /// 返回的提示
        /// </summary>
        private string _tips = "任务未完成";

        public string Tips
        {
            get => _tips;
            set => SetProperty(ref _tips, value);
        }

        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        private string _inputText = string.Empty;
    }
}
