﻿using CommunityToolkit.Mvvm.ComponentModel;
using SduNetCheckTool.Core.CustomInputTest;
using SduNetCheckTool.GUI.Common;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace SduNetCheckTool.GUI.ViewModels
{
    public class ToolBoxViewModel : ObservableObject
    {
        public ToolBoxViewModel()
        {
            Init();
        }

        private void Init()
        {
            Tasks = new ObservableCollection<UserPerformedTask>()
            {
                new UserPerformedTask(new InternetTest(),"指定网站Ping && 路由测试"),
                new UserPerformedTask(new DNSSwitch(),"DNS切换"),
            };
        }

        /// <summary>
        /// 任务
        /// </summary>
        private ObservableCollection<UserPerformedTask> _tasks;

        public ObservableCollection<UserPerformedTask> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }
    }
}
