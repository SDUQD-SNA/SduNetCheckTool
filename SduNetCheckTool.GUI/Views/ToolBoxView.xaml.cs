using SduNetCheckTool.GUI.ViewModels;
using System.Windows.Controls;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    /// ToolBoxView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBoxView : UserControl
    {
        public ToolBoxView()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService(typeof(ToolBoxViewModel));
        }
    }
}
