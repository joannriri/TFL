using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using TFL.Areas.Identity.Data;
using TFL.Models;
using TFL.ViewModels;

namespace TFL.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;

        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            // Fetch all cart items from the database
            var cartItems = _context.Carts
                .Include(c => c.Products)    // Include product details if needed
                .Include(c => c.Variants)    // Include variant details if needed
                .ToList();

            // Calculate the total amount
            decimal totalAmount = cartItems.Sum(item => item.Quantity * item.Price);

            var viewModel = new CheckoutView
            {
                CartItems = cartItems,
                TotalAmount = totalAmount
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult OrderSummary(int orderId) 
        {
            //fetch orderdetails based on orderid
            var order = _context.Orders
                .Include(o => o.OrderDetails)                
                .FirstOrDefault(o => o.OrderID == orderId);
            if(order == null)
            {
                return NotFound();
            }
            var orderDetails = order.OrderDetails
                .Select(d => new OrderItemViewModel
                {
                ProductName = d.ProductName,
                Color = _context.ProductVariants.FirstOrDefault(v => v.VariantID == d.VariantID)?.Color,
                Quantity = d.Quantity,
                LineTotal = d.Quantity * d.UnitPrice,
                ImageFile = d.ImageFile
                }).ToList();

            var viewModel = new OrderSummaryViewModel
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                FirstName = order.FirstName,
                OrderTotal = order.OrderTotal,
                ShippingFee = order.ShippingFee,
                DeliveryMethod = order.DeliveryMethod,
                ShippingAddress = order.ShippingAddress,
                OrderDetails = orderDetails                
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SubmitOrder(CheckoutView model)
        {
            if (model!=null)
            {

                //default value
                decimal shipfee = 0.0m;
                string deliveryM = "Pickup";

                //to check if shippingInfo is filled
                bool isShippingInfoFilled = !string.IsNullOrEmpty(model.FirstName) &&
                                            !string.IsNullOrEmpty(model.LastName) &&
                                            !string.IsNullOrEmpty(model.ShippingAddress) &&
                                            !string.IsNullOrEmpty(model.City) &&
                                            !string.IsNullOrEmpty(model.PostalCode) &&
                                            !string.IsNullOrEmpty(model.Country);


                if (isShippingInfoFilled)
                {
                    deliveryM = "shipment";
                    shipfee = 5.0m;

                }
                
                //Payment
                string paymentMethod = model.PaymentMethod;
                string bankselect = "";
                if (paymentMethod == "Bank Transfer" && string.IsNullOrEmpty(model.BankSelect))
                {
                    ModelState.AddModelError("BankSelect", "Please select a bank for the payment.");
                    return View("Checkout", model);
                }
                else if(paymentMethod == "Bank Transfer")
                {
                    bankselect = model.BankSelect;
                }

                //verify null model entry
                string FirstName = model.FirstName;
                string LastName = model.LastName;
                string ShippingAddress = model.ShippingAddress;
                string City = model.City;
                string PostalCode = model.PostalCode;
                string Country = model.Country;
                string PaymentMethod = model.PaymentMethod;
                if (string.IsNullOrEmpty(FirstName)) { FirstName = ""; }
                if (string.IsNullOrEmpty(LastName)) { LastName = ""; }
                if (string.IsNullOrEmpty(ShippingAddress)) { ShippingAddress = ""; }
                if (string.IsNullOrEmpty(City)) { City = ""; }
                if (string.IsNullOrEmpty(PostalCode)) { PostalCode = ""; }
                if (string.IsNullOrEmpty(Country)) { Country = ""; }
                if (string.IsNullOrEmpty(PaymentMethod)) { PaymentMethod = ""; }


                // Creates new order
                var order = new Orders
                {
                    OrderDate = DateTime.Now,
                    OrderTotal = model.TotalAmount,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DeliveryMethod = deliveryM,
                    FirstName = FirstName,
                    LastName = LastName,
                    ShippingAddress = ShippingAddress,
                    City = City,
                    PostalCode = PostalCode,
                    Country = Country,
                    PaymentMethod = PaymentMethod,                  
                    ShippingFee = shipfee,
                    BankSelect = bankselect
                };
                //add orders to db
                _context.Orders.Add(order);
                _context.SaveChanges();

                //Fetch cart items
                var cartItems = _context.Carts.ToList();

                //populate order details from cart items
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new OrderDetails
                    {
                        OrderID = order.OrderID, 
                        ProductID = cartItem.ProductID,
                        ProductName = cartItem.ProductName,
                        ImageFile = cartItem.Image, 
                        VariantID = cartItem.VariantID,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.Price
                    };

                    // Add each order detail to the context
                    _context.OrderDetails.Add(orderDetail);
                }

                //save changes for order details
                _context.SaveChanges();


                //to clear cart after submit order
                _context.Carts.RemoveRange(_context.Carts);
                 _context.SaveChanges();

                return RedirectToAction("OrderSummary", new { orderId = order.OrderID });
            }

            return View("Checkout", model);
        }

    }
}
