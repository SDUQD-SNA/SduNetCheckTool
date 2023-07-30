using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Core.Tests;

namespace SduNetCheckTool.GUI.Common
{
    public class DetectionTask : ObservableObject
    {
        public DetectionTask(ITest test, string name)
        {
            _test = test;
            Name = name;
        }

        public void RunTask()
        {
            TaskStatusEnum = TaskStatusEnum.Running;

            var result = _test.Test();

            if (result.Item1 == TestResult.Success)
                TaskStatusEnum = TaskStatusEnum.Completed;
            else if (result.Item1 == TestResult.Failed) 
                TaskStatusEnum = TaskStatusEnum.Error;

            Tips = result.Item2;
        }

        /// <summary>
        /// 测试类
        /// </summary>
        private readonly ITest _test;

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

    }

    /// <summary>
    /// 任务状态枚举类型
    /// </summary>
    public enum TaskStatusEnum
    {
        Waiting,
        Running,
        Completed,
        Error
    }
}
