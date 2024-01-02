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
        }
        private readonly DNSSwitchItem[] DNSList = new[]
        {
            new DNSSwitchItem { Name = "114 DNS", Value = "114.114.114.114" },
            new DNSSwitchItem { Name = "Google DNS", Value = "8.8.8.8" },
            new DNSSwitchItem { Name = "自动分配DNS", Value = "DHCP" },
            // 添加更多的项，如果需要
            };
        private new readonly DNSSwitch _test;
        // public new ICommand RunCommand { get; private set; }
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
                var interfaces = SelectedNetworkInterfaces
                    .Split(',')
                    .Select(name => NetworkInterfaceHelper.AvailableNetworkInterfacesWithName
                        .FirstOrDefault(item => item.Name == name))
                    .ToArray();
                var dns = SelectedDNSSwitchItem
                    .Split(',')
                    .Select(name => DNSList
                        .FirstOrDefault(item => item.Name == name))
                    .ToArray();
                Tips = _test.Switch(interfaces.Select(item => item.Interface).ToArray(), dns[0].Value);
                TaskStatusEnum = TaskStatusEnum.Completed;
            });
        }

        private void InitializeProperties()
        {
            NetworkInterfaces = NetworkInterfaceHelper.AvailableNetworkInterfacesWithName
                .Select(item => item.Name)
                .ToList();

            DNSSwitchItems = DNSList.Select(item => item.Name).ToList();

            // 初始化 SelectedNetworkInterfaces 和 SelectedDNSSwitchItem
            SelectedNetworkInterfaces = NetworkInterfaces.FirstOrDefault();
            SelectedDNSSwitchItem = DNSSwitchItems.FirstOrDefault();
        }

        public List<string> NetworkInterfaces { get; set; }
        public string SelectedNetworkInterfaces { get; set; }
        public List<string> DNSSwitchItems { get; set; }
        public string SelectedDNSSwitchItem { get; set; }
    }


}
