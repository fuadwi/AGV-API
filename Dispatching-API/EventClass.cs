using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatching_API
{
    class EventClass
    {

    }
    public class ConfigurationEvent : EventArgs
    {
        public Boolean newConfiguration { get; set; }
        //public int Threshold { get; set; }
        //public DateTime TimeReached { get; set; }
    }
}
