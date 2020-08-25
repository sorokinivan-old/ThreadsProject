using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ThreadService
{
    public class RequestView
    {

        [XmlAttribute("ThreadId")]
        public string ThreadId
        {
            get;
            set;
        }
    }
}
