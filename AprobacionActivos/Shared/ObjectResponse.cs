using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Shared
{
    public class ObjectResponse
    {
        public bool success { get; set; }
        public List<string> reasons { get; set; } = new List<string>();
        public object data { get; set; }
    }
}
