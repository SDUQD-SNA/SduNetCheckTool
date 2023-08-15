using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernWpf.Controls;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    /// RootView.xaml 的交互逻辑
    /// </summary>
    public partial class RootView : UserControl
    {
        private readonly Dictionary<string, Type> ViewMap = new Dictionary<string, Type>();
        public RootView()
        {
            void AddType(Type t) => this.ViewMap.Add(t.Name, t);

            AddType(typeof(TestView));

            InitializeComponent();

        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var targetView = (string)((NavigationViewItem)args.SelectedItem).Tag;
            this.MainFrame.Navigate(ViewMap[targetView]);
        }
    }
}
