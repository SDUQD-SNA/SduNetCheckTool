using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.CustomInputTest;

namespace SduNetCheckTool.Mvvm.Common
{
    public class UserPerformedTask : ObservableObject
    {
        public UserPerformedTask(ICustomInputTest test, string name)
        {
            _test = test;
            Name = name;
            RunCommand = new AsyncRelayCommand<string>(RunTask);
        }

        public virtual Task RunTask(string input)
        {
            return Task.Run(() =>
            {
                TaskStatusEnum = TaskStatusEnum.Running;
                Tips = _test.Test(input);
                TaskStatusEnum = TaskStatusEnum.Completed;
            });
        }

        public ICommand RunCommand { get; protected set; }

        /// <summary>
        /// 测试类
        /// </summary>
        protected ICustomInputTest _test;

        /// <summary>
        /// 任务名字
        /// </summary>
        protected string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// 完成状态
        /// </summary>
        protected TaskStatusEnum _taskStatusEnum = TaskStatusEnum.Waiting;

        public TaskStatusEnum TaskStatusEnum
        {
            get => _taskStatusEnum;
            set => SetProperty(ref _taskStatusEnum, value);
        }

        /// <summary>
        /// 返回的提示
        /// </summary>
        protected string _tips = "任务未完成";

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

        protected string _inputText = string.Empty;
    }
}
