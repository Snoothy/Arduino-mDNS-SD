using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Arduino_mDNS.Annotations;
using Arduino_mDNS.Managers;
using Arduino_mDNS.Models;

namespace Arduino_mDNS.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DiscoveryManager _discoveryManager;

        public ObservableCollection<ServiceAgent> Agents { get; set; }

        public string Response { get; set; }


        public MainWindowViewModel(DiscoveryManager discoveryManager)
        {
            _discoveryManager = discoveryManager;
            Agents = new ObservableCollection<ServiceAgent>();
        }

        public void AddAgent(ServiceAgent serviceagent)
        {
            Agents.Add(serviceagent);
        }

        public void SetResponse(string response)
        {
            Response = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt") + "\n" + response;
            OnPropertyChanged(nameof(Response));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearAgentList()
        {
            Agents.Clear();
        }
    }
}
