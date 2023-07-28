using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using SduNetCheckTool.GUI.ViewModels;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            DataContext = App.Current.Services.GetService<MainViewModel>();
            InitializeComponent();
        }
    }
}
