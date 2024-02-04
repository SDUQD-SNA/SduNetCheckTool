using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.ViewModels.ToolboxTabViewModels;

public partial class ToolboxTabViewModelBase:ObservableObject
{
    [ObservableProperty] private TaskStatusEnum status;

    public void SetStatus(TaskStatusEnum s)
    {
        Status = s;
    }
}