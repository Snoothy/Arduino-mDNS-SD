﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Arduino_mDNS.Models;
using Tmds.MDns;

namespace Arduino_mDNS.Managers
{
    public class DiscoveryManager
    {
        public List<ServiceAgent> ServiceAgents { get; set; }

        private ServiceBrowser _serviceBrowser;
        public delegate void AgentAdded(ServiceAgent serviceAgent); 
        public event AgentAdded AgentAddedEvent;


        public DiscoveryManager(AgentAdded agentAdded)
        {
            ServiceAgents = new List<ServiceAgent>();
            var serviceType = "_ucr._udp";

            _serviceBrowser = new ServiceBrowser();
            _serviceBrowser.ServiceAdded += OnServiceAdded;
            _serviceBrowser.ServiceRemoved += OnServiceRemoved;
            _serviceBrowser.ServiceChanged += OnServiceChanged;

            Debug.WriteLine($"Browsing for type: {serviceType}");
            _serviceBrowser.StartBrowse(serviceType);

            AgentAddedEvent += agentAdded;
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
            var serviceAgent = new ServiceAgent()
            {
                Hostname = e.Announcement.Hostname,
                Ip = e.Announcement.Addresses[0],
                Port = e.Announcement.Port
            };
            ServiceAgents.Add(serviceAgent);
            AgentAddedEvent?.Invoke(serviceAgent);
            printService('+', e.Announcement);
        }

        private static void printService(char startChar, ServiceAnnouncement service)
        {
            Debug.WriteLine($"{startChar} '{service.Instance}' on {service.NetworkInterface.Name}");
            Debug.WriteLine($"\tHost: {service.Hostname} ({string.Join(", ", service.Addresses)})");
            Debug.WriteLine($"\tPort: {service.Port}");
            Debug.WriteLine($"\tTxt : [{string.Join(", ", service.Txt)}]");
        }
    }
}
