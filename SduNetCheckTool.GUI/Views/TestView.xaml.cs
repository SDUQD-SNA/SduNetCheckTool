using Microsoft.Extensions.DependencyInjection;
using SduNetCheckTool.GUI.ViewModels;
using System.Windows.Controls;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView : UserControl
    {
        public TestView()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService(typeof(TestViewModel));
        }
    }
}
