using ModernWpf.Controls;
using System;
using System.Collections.Generic;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    /// RootView.xaml 的交互逻辑
    /// </summary>
    public partial class RootView
    {
        private readonly Dictionary<string, Type> _viewMap = new Dictionary<string, Type>();
        public RootView()
        {
            void AddType(Type t) => _viewMap.Add(t.Name, t);

            AddType(typeof(TestView));
            AddType(typeof(AboutView));
            AddType(typeof(ToolBoxView));

            InitializeComponent();


        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var targetView = (string)((NavigationViewItem)args.SelectedItem).Tag;
            if (targetView == null || !_viewMap.ContainsKey(targetView))
                return;
            this.MainFrame.Navigate(_viewMap[targetView]);
        }
    }
}
