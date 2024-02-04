using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Core.CustomInputTest;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.ViewModels
{
    public partial class ToolBoxViewModel : ObservableObject
    {
        public ToolBoxViewModel()
        {
            Init();
        }

        private void Init()
        {
            Tasks =
            [
                new UserPerformedTask(new InternetTest(),"指定网站Ping && 路由测试"),
                new CustomUserPerformdTask(new DNSSwitch(),"DNS切换"),
            ];
        }

        /// <summary>
        /// 任务
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<UserPerformedTask> _tasks;
    }
}
