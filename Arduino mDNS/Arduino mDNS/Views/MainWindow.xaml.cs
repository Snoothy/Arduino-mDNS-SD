using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Arduino_mDNS.Managers;
using Arduino_mDNS.Models;
using Arduino_mDNS.ViewModels;
using Tmds.MDns;

namespace Arduino_mDNS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public DiscoveryManager DiscoveryManager { get; set; }
        public UdpManager UdpManager { get; set; }

        public MainWindow()
        {
            DiscoveryManager = new DiscoveryManager(OnAgentAdded);
            UdpManager = new UdpManager();

            _viewModel = new MainWindowViewModel(DiscoveryManager);
            DataContext = _viewModel;
            
            InitializeComponent();
        }

        private void OnAgentAdded(ServiceAgent serviceagent)
        {
            _viewModel.AddAgent(serviceagent);
        }

        private void SendMessage(MessageBase messageBase)
        {
            if (SelectedAgent() == null) return;

            _viewModel.SetResponse(UdpManager.SendUdpPacket(SelectedAgent(), messageBase));
        }

        private ServiceAgent SelectedAgent()
        {
            return (ServiceAgent) AgentList.SelectedItem;
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearAgentList();
        }

        private void Heartbeat_OnClick(object sender, RoutedEventArgs e)
        {
            SendMessage(new HeartbeatMessage());
        }

        private void Descriptor_OnClick(object sender, RoutedEventArgs e)
        {
            SendMessage(new DescriptorMessage());
        }

        private void ButtonPress_OnClick(object sender, RoutedEventArgs e)
        {
            var message = new DataMessage();
            message.Buttons.Add(new IOData(0, 1));
            SendMessage(message);
        }

        private void ButtonRelease_OnClick(object sender, RoutedEventArgs e)
        {
            var message = new DataMessage();
            message.Buttons.Add(new IOData(0, 0));
            SendMessage(message);
        }
    }
}
