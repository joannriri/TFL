using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFL.Areas.Identity.Data;
using TFL.Models;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cartItems = await _context.Carts
            .Include(c => c.Variants)
            .ToListAsync();
        return View("Cart", cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity, int variantId)
    {
        if (quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        // Find the product
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        // Validate the variant
        var variant = await _context.ProductVariants.FindAsync(variantId);
        if (variant == null)
        {
            return NotFound("Variant not found.");
        }

        // Add new item to the cart
        var cartItem = new Cart
        {
            ProductID = productId,
            VariantID = variantId, // Add variant ID
            ProductName = product.ProductName,
            Quantity = quantity,
            Price = product.ProductPrice,
            Image = product.ImageFile
        };

        _context.Carts.Add(cartItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> UpdateCart(int cartId, int quantity)
    {
        if (quantity < 0)
        {
            return BadRequest("Quantity cannot be negative.");
        }

        var cartItem = await _context.Carts.FindAsync(cartId);
        if (cartItem == null)
        {
            return NotFound("Cart item not found.");
        }

        cartItem.Quantity = quantity;
        _context.Carts.Update(cartItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var cartItem = await _context.Carts.FindAsync(cartId);
        if (cartItem == null)
        {
            return NotFound("Cart item not found.");
        }

        _context.Carts.Remove(cartItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
