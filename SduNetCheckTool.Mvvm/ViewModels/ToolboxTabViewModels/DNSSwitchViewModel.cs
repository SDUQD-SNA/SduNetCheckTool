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

        public bool IsDnsSwitchEnabled {get { return _isDnsSwitchEnabled; }}
        public readonly bool _isDnsSwitchEnabled = Identifier.IsAdministrator();

     

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

                Tips = "脚本执行中";

                NetworkInterface[] interfaces = [SelectedNetworkInterface];
                if(Identifier.IsAdministrator())
                {                
                    Tips = SelectedDNSSwitchItem.Name switch
                    {
                    "自定义" => DNSSwitch.Switch(interfaces, CustomDNS),
                    _ => DNSSwitch.Switch(interfaces, SelectedDNSSwitchItem.DNSIPAddress),
                    };
                    SetStatus(TaskStatusEnum.Completed);
                } else
                {
                    Tips = "错误: 没有管理员权限!\n请尝试:点击按钮以管理员权限重启";
                    SetStatus(TaskStatusEnum.Error);
                }

            });
        }

        [RelayCommand]
        public void RebootAsAdmin()
        {
            if (Identifier.IsAdministrator())
            {
                Tips = "已经是管理员权限!\n可以修改DNS设置";
            } else
            {
                Identifier.RebootAsAdmin();
            }

        }

        private void InitializeProperties()
        {
            NetworkInterfaces = NetworkInterfaceHelper.GetAvailableNetworkInterfaces;
            DNSSwitchItems = DNSList;
            SelectedNetworkInterface = NetworkInterfaces.FirstOrDefault();
            SelectedDNSSwitchItem = DNSSwitchItems.FirstOrDefault();

            if (IsDnsSwitchEnabled)
            {
                Tips = "已经是管理员权限!\n可以修改DNS设置";
            } else
            {
                Tips = "错误: 没有管理员权限!\n请尝试:点击按钮以管理员权限重启";
            }
        }

        public NetworkInterface[] NetworkInterfaces { get; set; }
        public NetworkInterface SelectedNetworkInterface { get; set; }

        public DNSSwitchItem[] DNSSwitchItems { get; set; }

        public DNSSwitchItem SelectedDNSSwitchItem { get; set; }

        public string CustomDNS { get; set; }

    }


}
