using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using System.ComponentModel.DataAnnotations;

using System.Text;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Projek_MVC_v2.Models;

namespace Projek_MVC_v2.Areas.Identity.Pages.Account
{
    public class RegisterOrgModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterOrgModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]

            [Display(Name = "Firma")]
            public string Firma { get; set; }

            [Required]

            [Display(Name = "NIP")]
            public string Nip { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Has³o {0} musi posiadaæ minimalnie {2} oraz maksymalnie {1} znaków.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Has³o")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "PotwierdŸ has³o")]
            [Compare("Password", ErrorMessage = "Te has³a nie pasuj¹ do siebie. Spróbuj ponownie.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Podaj numer telefonu")]
            public string PhoneNumber { get; set; }

            
           
            public bool odziez { get; set; }
            public bool leki { get; set; }
            public bool zywnosc { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    user = 0,
                    nip = Input.Nip,
                    firma = Input.Firma,
                    imie = null,
                    nazwisko = null,
                    UserName = Input.Email,
                    Email = Input.Email
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Register",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    /*if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))//tutaj zrobic logowanie admina
                    {
                        return RedirectToAction("");
                    }*/

                    _logger.LogInformation("U¿ytkownik utworzy³ nowe konto z has³em.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "PotwierdŸ swój adres e-mail",
                        $"PotwierdŸ swój adres e-mail poprzez <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikniêcie tutaj</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

