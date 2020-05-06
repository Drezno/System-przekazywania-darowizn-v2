using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projek_MVC_v2.Models
{
    public class DonorFormModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string First_Name { get; set; }

        [DataType(DataType.Text)]
        public string Last_Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string County { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Street_Number { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
