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

        public string SendUdpPacket(ServiceAgent serviceAgent, MessageBase.MessageType messageType = MessageBase.MessageType.Heartbeat)
        {
            // Bind specific local port
            using var udpClient = new UdpClient(8090);
            udpClient.Client.ReceiveTimeout = 200;

            udpClient.Connect(serviceAgent.Ip, serviceAgent.Port);

            var message = CreateMessagePackMessage(messageType);
            udpClient.Send(message, message.Length);
            Debug.WriteLine($"Sent UDP to {serviceAgent.FullName}");
            Debug.WriteLine(MessagePackSerializer.ConvertToJson(message));

            var ipEndPoint = new IPEndPoint(serviceAgent.Ip, serviceAgent.Port);
            try
            {
                var response = udpClient.Receive(ref ipEndPoint);
                var responseString = System.Text.Encoding.Default.GetString(response);
                Debug.WriteLine($"Received UDP: {responseString}");
                return responseString;
            }
            catch (SocketException e)
            {
                Debug.WriteLine($"No UDP response");
                return "No response";
            }
            
        }

        private byte[] CreateMessagePackMessage(MessageBase.MessageType messageType)
        {
            switch (messageType)
            {
                case MessageBase.MessageType.Heartbeat:
                    return MessagePackSerializer.Serialize(new HeartbeatMessage());
                case MessageBase.MessageType.Descriptor:
                    return MessagePackSerializer.Serialize(new DescriptorMessage());
                case MessageBase.MessageType.Data:
                    break;
            }

            return MessagePackSerializer.Serialize(new HeartbeatMessage());
        }
    }
}
