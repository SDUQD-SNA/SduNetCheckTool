using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SduNetCheckTool.GUI.ViewModels
{
    public class Commands : ICommand
    {
        public static Commands OpenLink { get; } = new Commands
        {
            ExecuteDelegate = o =>
            {
                try { Process.Start(o.ToString()); }
                catch (Exception)
                {
                    // ignored
                }
            }
        };


        public static Commands Copy { get; } = new Commands
        {
            ExecuteDelegate = e =>
            {
                try { if (e is string str) Clipboard.SetText(str); }
                catch (Exception)
                {
                    // ignored
                }
            }
        };


        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public void Execute(object parameter) => this.ExecuteDelegate?.Invoke(parameter);

        public bool CanExecute(object parameter)
        {
            return this.CanExecuteDelegate == null || this.CanExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}