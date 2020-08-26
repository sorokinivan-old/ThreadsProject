using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThreadWindowsService.Models
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
