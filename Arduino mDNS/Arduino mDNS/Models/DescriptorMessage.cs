using System;
using System.Collections.Generic;
using System.Text;

namespace Arduino_mDNS.Models
{
    public class DescriptorMessage : MessageBase
    {
        public DescriptorMessage()
        {
            Type = MessageType.Descriptor;
        }
    }
}
