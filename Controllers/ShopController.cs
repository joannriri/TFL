using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFL.Areas.Identity.Data;

namespace TFL.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        //constructor to initialize dbcontext
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        //action method for seasonal-genevieve series page with sort and filter
        public IActionResult SeasonalGenevieve(string sortOrder, int? DiscountID, bool? inStock, bool? outOfStock)
        {
            //retrieve products for seasonal genevieve 
            var products = _context.Products
                .Where(p => p.CategoryID == 3)
                .Include(p => p.ProductCategory)
                .Include(p => p.Discount)
                .AsQueryable();

            //apply filtering
          
            if (DiscountID.HasValue)
            {
                products = products.Where(p => p.DiscountID == DiscountID.Value);
            }
            //apply stock filter
            if (inStock.HasValue && inStock.Value) 
            {
                products = products.Where(p => p.Stock > 0); //filter for instock products
            }
            if (outOfStock.HasValue && outOfStock.Value)
            {
                products = products.Where (p => p.Stock == 0); //out-of-stock filter
            }

            //apply sorting based on selected order
            switch (sortOrder)
            {
                case "name_asc":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                    
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;

                case "price_asc":
                    products = products.OrderBy(p => p.ProductPrice);
                    break;

                case "price_desc":
                    products = products.OrderByDescending(p => p.ProductPrice);
                    break;

                default:
                    products = products.OrderBy(p => p.ProductName); //def sort
                    break;

            }
            ViewBag.ProductCount = products.Count();
            return View("Views/Home/SeasonalGenevieve.cshtml", products.ToList());

        }
        public IActionResult SeasonalQalbi(string sortOrder, int?DiscountID, bool?inStock, bool? outOfStock)
        {
            //retrive products for Qalbi series
            var products = _context.Products
                .Where(p => p.CategoryID == 4)
                .Include(p => p.ProductCategory)
                .Include(p => p.Discount)
                .AsQueryable();

            //apply filtering
            if (DiscountID.HasValue)
            {
                products = products.Where(p => p.DiscountID == DiscountID.Value);
            }
            if (inStock.HasValue && inStock.Value)
            {
                products = products.Where(p => p.Stock > 0);
            }
            if (outOfStock.HasValue && outOfStock.Value)
            {
                products = products.Where (p => p.Stock == 0);
            }

            //apply sorting
            switch (sortOrder)
            {
                case "name_asc":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.ProductPrice);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }
            ViewBag.ProductCount = products.Count();
            return View("Views/Home/SeasonalQalbi.cshtml", products.ToList());

        }
        public IActionResult Exclusives(string sortOrder, int? DiscountID, bool? inStock, bool? outOfStock)
        {
            //retrieve products for exclusives
            var products = _context.Products
                .Where(p => p.CategoryID == 2)
                .Include(p => p.ProductCategory)
                .Include (p => p.Discount)
                .AsQueryable();
            
            //apply filtering
            if (DiscountID.HasValue)
            {
                products = products.Where(p => p.DiscountID == DiscountID.Value);
            }
            if (inStock.HasValue && inStock.Value)
            {
                products = products.Where(p => p.Stock > 0);
            }
            if (outOfStock.HasValue && outOfStock.Value)
            {
                products = products.Where(p => p.Stock == 0);
            }

            //apply sorting
            switch (sortOrder)
            {
                case "name_asc":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.ProductPrice);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName); // Default sort
                    break;
            }
            ViewBag.ProductCount = products.Count();
            return View("Views/Home/Exclusives.cshtml", products.ToList());

        }
        public IActionResult Cotton(string sortOrder, int? DiscountID, bool? inStock, bool? outOfStock)
        {
            //retrieve products for exclusives
            var products = _context.Products
                .Where(p => p.CategoryID == 1)
                .Include(p => p.ProductCategory)
                .Include(p => p.Discount)
                .AsQueryable();

            //apply filtering
            if (DiscountID.HasValue)
            {
                products = products.Where(p => p.DiscountID == DiscountID.Value);
            }
            if (inStock.HasValue && inStock.Value)
            {
                products = products.Where(p => p.Stock > 0);
            }
            if (outOfStock.HasValue && outOfStock.Value)
            {
                products = products.Where(p => p.Stock == 0);
            }

            //apply sorting
            switch (sortOrder)
            {
                case "name_asc":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.ProductPrice);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName); // Default sort
                    break;
            }
            ViewBag.ProductCount = products.Count();
            return View("Views/Home/Cotton.cshtml", products.ToList());

        }

        public IActionResult Product(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Discount)
                .Include(p => p.Variants)
                .FirstOrDefault(p => p.ProductID == id);
            
            if(product == null)
            {
                return NotFound();

            }

            //retrieve product from genevieve series
            var relatedProducts = _context.Products
                .Where(p => p.CategoryID == 3 && p.ProductID !=id)
                .Take(3)
                .ToList();
            //pass related product to view using viewbag
            ViewBag.RelatedProducts = relatedProducts;
            return View("Views/Home/Product.cshtml", product);
        }

    
    }
}
