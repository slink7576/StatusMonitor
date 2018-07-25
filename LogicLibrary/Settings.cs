
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LogicLibrary
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public string Font { get; set; }
        [DataMember]
        public int ColorB { get; set; }
        [DataMember]
        public int ColorG { get; set; }
        [DataMember]
        public int ColorR { get; set; }
       
       
    }
}