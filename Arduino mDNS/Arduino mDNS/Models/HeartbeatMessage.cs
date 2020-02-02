using System;
using System.Collections.Generic;
using System.Text;

namespace Arduino_mDNS.Models
{
    public class HeartbeatMessage : MessageBase
    {
        public HeartbeatMessage()
        {
            Type = MessageType.Heartbeat;
        }
    }
}
