using SduNetCheckTool.Core.CustomInputTest;
using System.Windows;
using System.Windows.Controls;

namespace SduNetCheckTool.GUI.Common
{
    public class TaskTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate InternetTestTemplate { get; set; }
        public DataTemplate DNSSwitchTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CustomUserPerformdTask) {
                return DNSSwitchTemplate;
            }

            return DefaultTemplate;
        }
    }
}
