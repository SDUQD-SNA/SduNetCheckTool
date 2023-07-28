using SduNetCheckTool.GUI.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SduNetCheckTool.GUI.Converters
{
    public class TaskStatus2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "0")
            {
                return (TaskStatusEnum)value == TaskStatusEnum.Running ? Visibility.Visible : Visibility.Collapsed;
            }

            return (TaskStatusEnum)value == TaskStatusEnum.Running ? Visibility.Collapsed : Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
