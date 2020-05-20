using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Projek_MVC_v2.Models;

namespace Projek_MVC_v2.Areas.Identity.Pages.Account.Manage
{
    public class EditMyCampaingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private string returnUrl;

        public List<AuthenticationScheme> ExternalLogins { get; private set; }

        public EditMyCampaingModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Username { get; set; }

        public string Phone { get; set; }




        
        public class InputModel
        {
            [Display(Name = "IdKampanii")]
            public string idKampania { get; set; }


            [Display(Name = "Tytul")]
            public string title { get; set; }

            

            [Display(Name = "Krotki  opis")]
            public string k_opis { get; set; }

           

            [Display(Name = "Dlugi opis")]
            public string d_opis { get; set; }

            [Display(Name = "Odziez")]
            public bool odziez { get; set; }

            [Display(Name = "Leki")]
            public bool leki { get; set; }

            [Display(Name = "Zywnosc")]
            public bool zywnosc { get; set; }

            [Display(Name = "Logo")]
            public IFormFile ForFile { get; set; }


        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            
            Username = userName;
            Phone = phoneNumber;


        }

        
        public async Task<IActionResult> OnPostAsync()
        {

         
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nie mo¿na za³adowaæ u¿ytkownika z ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);



            return Page();


            //return RedirectToPage("./MyCampania");
        }

        public async Task<IActionResult> OnPostWork1()
        {

            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection cnn = new MySqlConnection(db.GetConString());
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE kampanie SET tytul='" + Input.title + "' , opis ='" + Input.d_opis + "' , krotki_opis='" + Input.k_opis + "' where idkampanie='" + Input.idKampania + "'";
            cnn.Open();
            command.ExecuteNonQuery();
            cnn.Close();


            return RedirectToPage("./MyCampania");
        }


    }
}
