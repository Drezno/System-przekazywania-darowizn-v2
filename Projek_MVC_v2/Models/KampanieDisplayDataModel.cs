using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projek_MVC_v2.Models
{
    public class KampanieDisplayDataModel
    {
        public KampanieDisplayDataModel() : base() { }
        public int idkampania { get; set; }
        public string username { get; set; }
        public string telefon { get; set; }
        public string tytul { get; set; }
        public string krotkiopis { get; set; }
        public string dlugiopis { get; set; }

        public sbyte odziez { get; set; }

        public sbyte leki { get; set; }

        public sbyte zywnosc { get; set; }

        public string obrazek_path { get; set; }

    }
}
