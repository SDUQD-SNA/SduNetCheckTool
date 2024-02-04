using System.Windows.Controls;
using SduNetCheckTool.Mvvm.ViewModels.ToolboxTabViewModels;

namespace SduNetCheckTool.GUI.Views.ToolboxTabs
{
    /// <summary>
    /// DNSSwitchView.xaml 的交互逻辑
    /// </summary>
    public partial class DNSSwitchView : UserControl
    {
        public DNSSwitchView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService(typeof(DNSSwitchViewModel));
        }
    }
}
