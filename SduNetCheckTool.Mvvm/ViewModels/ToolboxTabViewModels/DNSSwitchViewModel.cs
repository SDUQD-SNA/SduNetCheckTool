using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SduNetCheckTool.Core.Tools;
using SduNetCheckTool.Core.Utils;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.ViewModels.ToolboxTabViewModels
{
    public partial class DNSSwitchViewModel : ToolboxTabViewModelBase
    {
        public struct DNSSwitchItem
        {
            public string Name { get; set; }
            public string DNSIPAddress { get; set; }

            public string NameAndValue => $"{Name} ({DNSIPAddress})";
        }

        private readonly DNSSwitchItem[] DNSList =
        [
            new DNSSwitchItem { Name = "阿里 DNS", DNSIPAddress = "223.5.5.5" },
            new DNSSwitchItem { Name = "腾讯 DNS", DNSIPAddress = "119.29.29.29" },
            new DNSSwitchItem { Name = "114 DNS", DNSIPAddress = "114.114.114.114" },
            new DNSSwitchItem { Name = "谷歌 DNS", DNSIPAddress = "8.8.8.8" },
            new DNSSwitchItem { Name = "自动分配DNS", DNSIPAddress = "DHCP" },
            // 添加更多的项，如果需要
            new DNSSwitchItem { Name = "自定义", DNSIPAddress = string.Empty }
        ];

        public DNSSwitchViewModel()
        {
            InitializeProperties();
        }

        [ObservableProperty] private string _tips;

        [RelayCommand]
        public Task RunDnsSwitch()
        {
            return Task.Run(() =>
            {
                SetStatus(TaskStatusEnum.Running);


                NetworkInterface[] interfaces = [SelectedNetworkInterface];

                Tips = SelectedDNSSwitchItem.Name switch
                {
                    "自定义" => DNSSwitch.Switch(interfaces, CustomDNS),
                    _ => DNSSwitch.Switch(interfaces, SelectedDNSSwitchItem.DNSIPAddress),
                };


                SetStatus(TaskStatusEnum.Completed);
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
