using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projek_MVC_v2.Models
{
    public class ApplicationUser : IdentityUser
    {
       public ApplicationUser(): base() { }

        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string firma { get; set; }
        public string nip { get; set; }
        public int user { get; set; }
    }
}
