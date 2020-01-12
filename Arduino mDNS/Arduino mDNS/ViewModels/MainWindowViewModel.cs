using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Arduino_mDNS.Managers;
using Arduino_mDNS.Models;

namespace Arduino_mDNS.ViewModels
{
    public class MainWindowViewModel
    {
        private DiscoveryManager _discoveryManager;

        public ObservableCollection<ServiceAgent> Agents { get; set; }


        public MainWindowViewModel(DiscoveryManager discoveryManager)
        {
            _discoveryManager = discoveryManager;
            Agents = new ObservableCollection<ServiceAgent>();
        }

        public void AddAgent(ServiceAgent serviceagent)
        {
            Agents.Add(serviceagent);
        }

    }
}
