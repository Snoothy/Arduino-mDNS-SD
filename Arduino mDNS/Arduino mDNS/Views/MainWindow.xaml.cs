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
using Arduino_mDNS.ViewModels;

namespace Arduino_mDNS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public DiscoveryManager DiscoveryManager { get; set; }

        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            DiscoveryManager = new DiscoveryManager();
            
            InitializeComponent();
        }

        private void Scan_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
