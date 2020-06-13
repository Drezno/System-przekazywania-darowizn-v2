using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MimeKit.IO.Filters;
using MySql.Data.MySqlClient;
using Projek_MVC_v2.Data;
using Projek_MVC_v2.Models;

namespace Projek_MVC_v2.Areas.Identity.Pages.Account.Manage
{
    public class CampaniaModel : PageModel
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private string returnUrl;

        public List<AuthenticationScheme> ExternalLogins { get; private set; }

        public CampaniaModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        public string Phone { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]

            [Display(Name = "Tytul")]
            public string title { get; set; }

            [Required]

            [Display(Name = "Krotki  opis")]
            public string k_opis { get; set; }

            [Required]

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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie mo¿na za³adowaæ u¿ytkownika z ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var ph = await _userManager.GetPhoneNumberAsync(user);
            if (user == null)
            {
                return NotFound($"Nie mo¿na za³adowaæ u¿ytkownika z ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {/*
                var kampania = new Kampaniee
                {
                    username = Username,
                    telefon = Phone,
                    tytul = Input.title,
                    krotkiopis = Input.k_opis,
                    dlugiopis = Input.d_opis,
                    odziez = Input.odziez,
                    leki = Input.leki,
                    zywnosc = Input.zywnosc

                };
                */
                


                var file = Input.ForFile;
                string newFullPath = null;
                string tempFileName = null;
                if (file !=null)
                {
                    
                    int count = 1;

                    // var formFile = HttpContext.Request.Form.Files[0];
                    
                    var fullPath = Path.Combine(@"C:\Users\Patryk\Desktop\NET.PROJEKT\new one\System-przekazywania-darowizn-v2\Projek_MVC_v2\wwwroot\images\uploads\", file.FileName);
                    
                    string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
                    tempFileName = fileNameOnly;
                    string extension = Path.GetExtension(fullPath);
                    
                    //string path = Path.GetDirectoryName(fullPath);

                    string PATH = @"C:\Users\Patryk\Desktop\NET.PROJEKT\new one\System-przekazywania-darowizn-v2\Projek_MVC_v2\wwwroot\images\uploads\";


                    newFullPath = fullPath;


                    while (System.IO.File.Exists(newFullPath))
                    {
                        tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        newFullPath = Path.Combine(PATH+@"\", tempFileName + extension);

                    } 


                    using (var fileStream = new FileStream(newFullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    newFullPath = @"\/images\/uploads\/" + tempFileName + extension;
                    //StatusMessage = formFile;
                }
                
                int o = Input.odziez ? 1 : 0;
                int l = Input.leki ? 1 : 0;
                int z = Input.zywnosc ? 1 : 0;

                DatabaseConnection db = new DatabaseConnection();
                MySqlConnection cnn = new MySqlConnection(db.GetConString());
                var command = cnn.CreateCommand();
                command.CommandText = "INSERT INTO kampanie (username,tytul,opis,krotki_opis,odziez,leki,zywnosc,telefon,picture) VALUES ('" + user + "','" + Input.title + "','" + Input.d_opis + "','" + Input.k_opis + "','" + o + "','" + l + "','"+ z + "','"+ ph + "','" + newFullPath + "')";
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();

            }
                await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Twoja kampania zosta³a stworzona.";
            return RedirectToPage();
        }

     


    }
}
