using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheFabricsLab.Areas.Identity.Data;
using TheFabricsLab.Models;

namespace TheFabricsLab.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext context;
        public UserController(UserManager<AppUser> userManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> UserDetails()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle the case where the user is not found
            }

            return View(user);
        }

        public IActionResult Admin()
        {
            var products = context.Products.OrderByDescending(p => p.ProductID).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategory category)
        {
            if(ModelState.IsValid)
            {
                context.ProductCategories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(category);
        }

        
    }
}
