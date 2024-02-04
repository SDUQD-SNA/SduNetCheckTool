using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Core.Repairs;
using SduNetCheckTool.Core.Tests;

namespace SduNetCheckTool.Mvvm.Common
{
    public partial class DetectionTask : ObservableObject
    {
        public DetectionTask(ITest test, string name)
        {
            Test = test;
            Name = name;
        }

        public IRepair RunTask()
        {
            TaskStatusEnum = TaskStatusEnum.Running;

            var result = Test.Test();

            TaskStatusEnum = result.Item1 switch
            {
                TestResult.Success => TaskStatusEnum.Completed,
                TestResult.Failed => TaskStatusEnum.Error,
                _ => TaskStatusEnum.Error
            };

            Tips = result.Item2;

            return result.Item3;
        }

        /// <summary>
        /// 测试类
        /// </summary>
        [ObservableProperty]
        private ITest _test;

        /// <summary>
        /// 任务名字
        /// </summary>
        [ObservableProperty]
        private string _name;

        /// <summary>
        /// 完成状态
        /// </summary>
        [ObservableProperty]
        private TaskStatusEnum _taskStatusEnum = TaskStatusEnum.Waiting;

        /// <summary>
        /// 返回的提示
        /// </summary>
        [ObservableProperty]
        private string _tips = "任务未完成";
    }


}
