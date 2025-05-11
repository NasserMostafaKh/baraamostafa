using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TequliasRestaurant.Data;
using TequliasRestaurant.Models;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;

namespace TequliasRestaurant.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        private Repository<Ingredient> ingredients;
        private Repository<Category> categories;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            products = new Repository<Product>(context);
            ingredients = new Repository<Ingredient>(context);
            categories = new Repository<Category>(context);
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await products.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Ingredients = await ingredients.GetAllAsync();
            ViewBag.Categories = await categories.GetAllAsync();
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                Product product = await products.GetByIdAsync(id, new QueryOptions<Product>
                {
                    Includes = "ProductIngredients.Ingredient,Category"
                });
                ViewBag.Operation = "Edit";
                return View(product);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(Product product, int[] ingredientIds, int catId)
        {

            ViewBag.Ingredients = await ingredients.GetAllAsync();
            ViewBag.Categories = await categories.GetAllAsync();

            // التحقق من وجود الفئة
            var category = await categories.GetByIdAsync(catId, new QueryOptions<Category>());
            if (category == null)
            {
                ModelState.AddModelError("", "الفئة المحددة غير موجودة");
                return View(product);
            }

            // التحقق من المكونات
            if (ingredientIds == null || ingredientIds.Length == 0)
            {
                ModelState.AddModelError("", "يجب تحديد مكون واحد على الأقل");
                return View(product);
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = uniqueFileName;
                }

                product.CategoryId = catId;

                if (product.ProductId == 0)
                {
                    // إضافة منتج جديد
                    await products.AddAsync(product);

                    // إضافة المكونات بعد إنشاء المنتج
                    foreach (int id in ingredientIds)
                    {
                        product.ProductIngredients.Add(new ProductIngredient
                        {
                            IngredientId = id,
                            ProductId = product.ProductId
                        });
                    }

                    await products.UpdataAsync(product); // لحفظ المكونات
                }
                else
                {
                    // تعديل منتج موجود
                    var existingProduct = await products.GetByIdAsync(product.ProductId,
                        new QueryOptions<Product> { Includes = "ProductIngredients" });

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = catId;
                    existingProduct.ImageUrl = product.ImageUrl ?? existingProduct.ImageUrl;

                    existingProduct.ProductIngredients.Clear();
                    foreach (int id in ingredientIds)
                    {
                        existingProduct.ProductIngredients.Add(new ProductIngredient
                        {
                            IngredientId = id,
                            ProductId = product.ProductId
                        });
                    }

                    await products.UpdataAsync(existingProduct);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ: {ex.Message}");
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
        try
            {
                await products.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch {
                ModelState.AddModelError("", "Product Not Found.");
                return RedirectToAction("Index");
            }
        }
    }
}