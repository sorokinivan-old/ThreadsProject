using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThreadApi
{
    public class ReturnView
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
        [XmlAttribute("Time")]
        public string Time
        {
            get;
            set;
        }
    }
    [XmlRoot("Products")]
    public class ReturnViews
    {
        [XmlAttribute("nid")]
        public string Id
        {
            get;
            set;
        }
        [XmlElement(ElementName = "Product")]
        public List<ReturnView> AllReturnViews { get; set; }
    }
}
