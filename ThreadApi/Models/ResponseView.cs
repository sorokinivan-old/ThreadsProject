using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThreadApi
{
    public class ResponseView
    {
        [XmlAttribute("State")]
        public string State
        {
            get;
            set;
        }
        [XmlAttribute("ThreadId")]
        public string ThreadId
        {
            get;
            set;
        }
        [XmlAttribute("ResponseTime")]
        public long ResponseTime
        {
            get;
            set;
        }
        [XmlAttribute("ProcessingTime")]
        public long ProcessingTime
        {
            get;
            set;
        }
    }
    
}
