using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Arduino_mDNS.Models
{
    [MessagePackObject]
    public class IOData
    {
        public IOData()
        {
        }

        public IOData(int index, short value)
        {
            Index = index;
            Value = value;
        }

        [Key("Index")]
        public int Index { get; set; }
        [Key("Value")]
        public short Value { get; set; }
    }
}
