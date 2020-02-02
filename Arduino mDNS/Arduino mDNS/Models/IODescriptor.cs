using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Arduino_mDNS.Models
{
    [MessagePackObject]
    public class IODescriptor
    {
        [Key("Name")]
        public string Name { get; set; }
        [Key("Value")]
        public int Value { get; set; }
    }
}
