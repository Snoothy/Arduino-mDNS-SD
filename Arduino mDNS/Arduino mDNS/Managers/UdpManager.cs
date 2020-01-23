using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Arduino_mDNS.Models;

namespace Arduino_mDNS.Managers
{
    public class UdpManager
    {
        public UdpManager()
        {
        }

        public void SendUdpPacket(ServiceAgent serviceAgent)
        {
            // Bind specific local port
            using var udpClient = new UdpClient(8090);

            udpClient.Connect(serviceAgent.Ip, serviceAgent.Port);

            var sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            udpClient.Send(sendBytes, sendBytes.Length);

            Debug.WriteLine($"Sent UDP to {serviceAgent.FullName}");
        }
    }
}
