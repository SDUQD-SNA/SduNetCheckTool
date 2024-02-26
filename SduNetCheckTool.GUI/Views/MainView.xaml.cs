using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using SduNetCheckTool.GUI.Views.Dialogs;
using SduNetCheckTool.Mvvm.Common.Messages;

namespace SduNetCheckTool.GUI.Views
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<InfoDialogMessage>(this, async (r, m) =>
            {
                var dialog = new InfoDialog
                {
                    Owner=this
                };

                dialog.Description.Text = m.Description;

                await dialog.ShowAsync();
            });
        }
    }
}