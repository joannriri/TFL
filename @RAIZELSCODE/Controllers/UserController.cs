using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IWebHostEnvironment environment;

        public UserController(UserManager<AppUser> userManager, AppDbContext context, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.context = context;
            this.environment = environment;
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
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.ImageFile == null) 
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            //save image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/Catalog/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath)) 
            { 
                productDto.ImageFile.CopyTo(stream);
            }

            //save new product to database
            Product product = new Product()
            {
                ProductName = productDto.ProductName,
                CategoryID = productDto.CategoryID,
                ProductPrice = productDto.ProductPrice,
                Description = productDto.Description,
                ImageFile = newFileName,
                Stock = productDto.Stock,
                CreatedAt = DateTime.Now
            };

            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Admin", "User");
        }

        ////scrap
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(ProductCategory category)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        context.ProductCategories.Add(category);
        //        context.SaveChanges();
        //        return RedirectToAction("Admin");
        //    }
        //    return View(category);
        //}

        public IActionResult Edit(int id) 
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Admin", "User");
            }

            //create productDto from product
            var productDto = new ProductDto()
            {
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                ProductPrice = product.ProductPrice,
                Description = product.Description,
                Stock = product.Stock
            };

            ViewData["ProductID"] = product.ProductID;
            ViewData["ImageFileName"] = product.ImageFile;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto) 
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Admin", "User");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ProductID"] = product.ProductID;
                ViewData["ImageFileName"] = product.ImageFile;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                return View(productDto);
            }

            //update img if replaced by new img
            string newFileName = product.ImageFile;
            if(productDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/Catalog/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath)) 
                {
                    productDto.ImageFile.CopyTo(stream);
                }

                //delete old img
                string oldImageFullPath = environment.WebRootPath + "/Catalog/" + product.ImageFile;
                System.IO.File.Delete(oldImageFullPath);
            }

            //update product in db
            product.ProductName = product.ProductName;
            product.CategoryID = product.CategoryID;
            product.ProductPrice = product.ProductPrice;
            product.Description = product.Description;
            product.ImageFile = newFileName;
            product.Stock = product.Stock;

            context.SaveChanges();
            return RedirectToAction("Admin", "User");

        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                return RedirectToAction("Admin", "User");
            }

            string imageFullPath = environment.WebRootPath + "/Catalog" + product.ImageFile;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges(true);

            return RedirectToAction("Admin", "User");
        }
    }
}
