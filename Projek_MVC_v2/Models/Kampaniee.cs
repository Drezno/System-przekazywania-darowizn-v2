using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projek_MVC_v2.Models
{
    public class Kampaniee
    {
        public Kampaniee() : base() { }

        public string username { get; set; }
        public string telefon{ get; set; }
        public string tytul { get; set; }
        public string krotkiopis { get; set; }
        public string dlugiopis { get; set; }

        public bool odziez { get; set; }

        public bool leki { get; set; }

        public bool zywnosc { get; set; }

        

    }
}
