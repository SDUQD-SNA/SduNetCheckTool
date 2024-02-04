using ModernWpf.Controls;
using System;
using System.Globalization;
using System.Windows.Data;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.GUI.Converters
{
    public class TaskStatus2SymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TaskStatusEnum)value)
            {
                case TaskStatusEnum.Completed:
                    return Symbol.Accept;
                case TaskStatusEnum.Error:
                    return Symbol.Important;
                default:
                    return Symbol.Clock;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
