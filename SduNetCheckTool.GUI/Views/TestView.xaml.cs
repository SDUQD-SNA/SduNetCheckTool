using System.Windows.Controls;
using System.Windows.Input;
using SduNetCheckTool.Mvvm.ViewModels;
using Page = ModernWpf.Controls.Page;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    ///     TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView : Page
    {
        public TestView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService(typeof(TestViewModel));
        }

        private void ScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollViewer.LineUp();
            else
                scrollViewer.LineDown();
            e.Handled = true;
        }
    }
}