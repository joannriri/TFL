// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheFabricsLab.Areas.Identity.Data;

namespace TheFabricsLab.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        
        public IndexModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public AppUser? appUser;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateOnly DateOfBirth { get; set; }
            public string PhoneNumber { get; set; }
            [Display(Name = "Subscribe to Newsletter?")]
            public bool NewsletterSubscription { get; set; }
        }

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                appUser = user;
                Input = new InputModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    NewsletterSubscription = user.NewsletterSubscription
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;
            user.NewsletterSubscription = Input.NewsletterSubscription;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }


        //public void OnGet()
        //{
        //    var task = userManager.GetUserAsync(User);
        //    task.Wait();
        //    appUser = task.Result;
        //}
    }
}
