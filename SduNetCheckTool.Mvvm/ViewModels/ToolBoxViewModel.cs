using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.ViewModels
{
    public partial class ToolBoxViewModel : ObservableObject
    {
        /// <summary>
        /// 任务
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<ToolboxTab> _tabs;
    }
}
