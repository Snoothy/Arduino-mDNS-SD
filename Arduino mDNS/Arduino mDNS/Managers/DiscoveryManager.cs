using System;
using System.Collections.Generic;
using System.Text;
using Tmds.MDns;

namespace Arduino_mDNS.Managers
{
    public class DiscoveryManager
    {
        private ServiceBrowser _serviceBrowser;

        public DiscoveryManager()
        {
            var serviceType = "_workstation._tcp";

            _serviceBrowser = new ServiceBrowser();
            _serviceBrowser.ServiceAdded += OnServiceAdded;
            _serviceBrowser.ServiceRemoved += OnServiceRemoved;
            _serviceBrowser.ServiceChanged += OnServiceChanged;

            Console.WriteLine("Browsing for type: {0}", serviceType);
            _serviceBrowser.StartBrowse(serviceType);
        }

        private void OnServiceChanged(object sender, ServiceAnnouncementEventArgs e)
        {
            printService('+', e.Announcement);
        }

        private void OnServiceRemoved(object sender, ServiceAnnouncementEventArgs e)
        {
            printService('+', e.Announcement);
        }

        private void OnServiceAdded(object sender, ServiceAnnouncementEventArgs e)
        {
            printService('+', e.Announcement);
        }

        private static void printService(char startChar, ServiceAnnouncement service)
        {
            Console.WriteLine("{0} '{1}' on {2}", startChar, service.Instance, service.NetworkInterface.Name);
            Console.WriteLine("\tHost: {0} ({1})", service.Hostname, string.Join(", ", service.Addresses));
            Console.WriteLine("\tPort: {0}", service.Port);
            Console.WriteLine("\tTxt : [{0}]", string.Join(", ", service.Txt));
        }
    }
}
