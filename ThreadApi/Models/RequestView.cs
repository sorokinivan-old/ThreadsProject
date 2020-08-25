using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThreadApi
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
