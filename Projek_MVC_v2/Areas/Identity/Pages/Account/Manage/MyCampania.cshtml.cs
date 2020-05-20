using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Projek_MVC_v2.Models;

namespace Projek_MVC_v2.Areas.Identity.Pages.Account.Manage
{
    public class MyCampaniaModel : PageModel
    {


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {



            [Display(Name = "IdKampanii")]
            public string idKampania { get; set; }

        }


        public async Task<IActionResult> OnPostAsync()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection cnn = new MySqlConnection(db.GetConString());
            var command = cnn.CreateCommand();
            command.CommandText = "DELETE FROM kampanie where idkampanie='" + Input.idKampania + "'";
            cnn.Open();
            command.ExecuteNonQuery();
            cnn.Close();

            
           
            return RedirectToPage();
        }
    }
}
