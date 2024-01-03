using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.CustomInputTest;
using SduNetCheckTool.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SduNetCheckTool.GUI.Common
{
    public class CustomUserPerformdTask : UserPerformedTask
    {
        public class DNSSwitchItem
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public string NameAndValue => $"{Name} ({Value})";
        }
        public readonly DNSSwitchItem[] DNSList = new[]
        {
            new DNSSwitchItem { Name = "阿里 DNS", Value = "223.5.5.5" },
            new DNSSwitchItem { Name = "腾讯 DNS", Value = "119.29.29.29" },
            new DNSSwitchItem { Name = "114 DNS", Value = "114.114.114.114" },
            new DNSSwitchItem { Name = "谷歌 DNS", Value = "8.8.8.8" },
            new DNSSwitchItem { Name = "自动分配DNS", Value = "DHCP" },
            // 添加更多的项，如果需要
            new DNSSwitchItem { Name = "自定义", Value = string.Empty }
        };
        private new readonly DNSSwitch _test;
        public CustomUserPerformdTask(DNSSwitch test, string name) : base(test, name)
        {
            InitializeProperties();
            _test = test;
            RunCommand = new AsyncRelayCommand<string>(RunTask);
        }

        new public Task RunTask(string input)
        {
            return Task.Run(() =>
            {
                TaskStatusEnum = TaskStatusEnum.Running;
                NetworkInterface[] interfaces = new NetworkInterface[] { SelectedNetworkInterface };
                switch (SelectedDNSSwitchItem.Name)
                {
                    case "自定义":
                        Tips = _test.Switch(interfaces, CustomDNS);
                        break;
                    default:
                        Tips = _test.Switch(interfaces, SelectedDNSSwitchItem.Value);
                        break;
                }
                TaskStatusEnum = TaskStatusEnum.Completed;
            });
        }

        private void InitializeProperties()
        {
            NetworkInterfaces = NetworkInterfaceHelper.GetAvailableNetworkInterfaces;
            DNSSwitchItems = DNSList;
            SelectedNetworkInterface = NetworkInterfaces.FirstOrDefault();
            SelectedDNSSwitchItem = DNSSwitchItems.FirstOrDefault();

        }

        public NetworkInterface[] NetworkInterfaces { get; set; }
        public NetworkInterface SelectedNetworkInterface { get; set; }

        public DNSSwitchItem[] DNSSwitchItems { get; set; }

        public DNSSwitchItem SelectedDNSSwitchItem { get; set; }

        public string CustomDNS { get; set; }

    }


}
