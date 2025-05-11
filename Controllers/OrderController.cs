using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TequliasRestaurant.Data;
using TequliasRestaurant.Models;

namespace TequliasRestaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);
            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            HttpContext.Session.Set("OrderViewModel", model);
            return RedirectToAction("Create", model);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model == null || model.OrderItems == null || model.OrderItems.Count == 0)
            {
                // يمكنك إما إعادة توجيه أو إرجاع نموذج فارغ
                return RedirectToAction("Create");

                // أو إرجاع عرض مع نموذج فارغ:
                // return View(new OrderViewModel { OrderItems = new List<OrderItemViewModel>() });
            }

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if (model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            // Create a new Order entity  
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User)
            };
            // Add OrderItems to the Order entity  
            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            // Save the Order entity to the database
            await _orders.AddAsync(order);
            // Clear the OrderViewModel from session or other state management
            HttpContext.Session.Remove("OrderViewModel");
            // Redirect to the Order Confirmation page
            return RedirectToAction("ViewOrders");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);
            var userOrders = await _orders.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            return View(userOrders);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            return Json(new { count = model?.OrderItems?.Sum(i => i.Quantity) ?? 0 });
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost] // تأكد أنها POST
        [ValidateAntiForgeryToken] // إضافة هذه السمة
        public IActionResult RemoveItem(int id)
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model?.OrderItems != null)
            {
                var item = model.OrderItems.FirstOrDefault(i => i.ProductId == id);
                if (item != null)
                {
                    model.OrderItems.Remove(item);
                    model.TotalAmount = model.OrderItems.Sum(i => i.Price * i.Quantity);
                    HttpContext.Session.Set("OrderViewModel", model);
                }
            }

            return RedirectToAction("Cart");
        }
    }
}
