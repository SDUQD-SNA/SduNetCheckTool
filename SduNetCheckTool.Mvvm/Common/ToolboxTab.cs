using CommunityToolkit.Mvvm.ComponentModel;

namespace SduNetCheckTool.Mvvm.Common
{
    public partial class ToolboxTab : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty] private object _ui;

        public ToolboxTab(string name, object ui)
        {
            Name = name;
            Ui = ui;
        }
    }
}
