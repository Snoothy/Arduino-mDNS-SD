using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Arduino_mDNS.Models
{
    public class ServiceAgent
    {
        public string FullName => $"{Hostname} ({Ip}:{Port})";
        public string Hostname { get; set; }

        public IPAddress Ip { get; set; }

        public int Port { get; set; }
    }
}
