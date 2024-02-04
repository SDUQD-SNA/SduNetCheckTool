using System.Windows.Controls;
using System.Windows.Input;
using SduNetCheckTool.Mvvm.ViewModels;
using Page = ModernWpf.Controls.Page;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    ///     ToolBoxView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBoxView : Page
    {
        public ToolBoxView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService(typeof(ToolBoxViewModel));
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