using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Arduino_mDNS.Models
{
    [MessagePackObject]
    public class MessageBase
    {
        public enum MessageType
        {
            Heartbeat = 0,
            Descriptor = 1,
            Data = 2
        }

        [Key(0)]
        public MessageType Type { get; set; }
    }
}
