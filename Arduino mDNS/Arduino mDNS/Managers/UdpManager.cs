using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Arduino_mDNS.Models;
using MessagePack;

namespace Arduino_mDNS.Managers
{
    public class UdpManager
    {
        public UdpManager()
        {
        }

        public string SendUdpPacket(ServiceAgent serviceAgent, object messageBase)
        {
            // Bind specific local port
            using var udpClient = new UdpClient(8090)
            {
                Client = {ReceiveTimeout = 200}
            };

            udpClient.Connect(serviceAgent.Ip, serviceAgent.Port);

            var message = MessagePackSerializer.Serialize(messageBase);
            udpClient.Send(message, message.Length);
            Debug.WriteLine($"Sent UDP to {serviceAgent.FullName}");
            Debug.WriteLine(MessagePackSerializer.ConvertToJson(message));

            var ipEndPoint = new IPEndPoint(serviceAgent.Ip, serviceAgent.Port);
            try
            {
                var response = udpClient.Receive(ref ipEndPoint);
                if (((MessageBase)messageBase).Type == MessageBase.MessageType.Descriptor)
                {
                    var descriptorMessage = MessagePackSerializer.Deserialize<DescriptorMessage>(response);
                    var a = 1;
                }
                var responseString = Encoding.Default.GetString(response);
                Debug.WriteLine($"Received UDP: {responseString}");
                return responseString;
            }
            catch (SocketException e)
            {
                Debug.WriteLine($"No UDP response");
                return "No response";
            }
            
        }
    }
}
