using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProdCat.Models;

namespace ProdCat.Controllers
{
    public class HomeController : Controller
    {
        private ProdCatContext db;
        public HomeController(ProdCatContext context)
        {
            db = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Product> allProducts = db.Products
            .ToList();
            return View("Products", allProducts);
        }
        [HttpPost("newproduct")]
        public IActionResult NewProduct(ProdCatViewModel modelData)
        {
            Product addedProduct = modelData.nuProduct;
            db.Add(addedProduct);
            db.SaveChanges();
            return RedirectToAction("Products");
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            List<Product> allProducts = db.Products
            .ToList();
            return View("Products", allProducts);

        }
        [HttpGet("categories")]
        public IActionResult Categories()
        {
            List<Category> allCats = db.Categories
            .ToList();
            return View("Categories", allCats);
        }
        [HttpPost("newcategory")]
        public IActionResult NewCategory(ProdCatViewModel modelData)
        {
            Category addedCategory = modelData.nuCategory;
            db.Add(addedCategory);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        [HttpGet("/products/{prodId}")]
        public IActionResult GetProduct(int prodId)
        {
            Product thisProd = db.Products.
            Include(c => c.Categories)
            .ThenInclude(prodCat => prodCat.Category)
            .FirstOrDefault(p => p.ProductId == prodId);

            if (thisProd == null)
            {
                return RedirectToAction("Products");
            }

            List<Category> unrelatedCategories = db.Categories
            .Include(catProd => catProd.Products).
            Where(catProd => catProd.Products.Any(catProd => catProd.ProductId == thisProd.ProductId) == false).ToList();

            ProdCatViewModel mod = new ProdCatViewModel()
            {
                nuProduct = thisProd,
                catViewModel = unrelatedCategories
            };
            List<Category> allCategories = db.Categories.ToList();
            List<Category> someCategories = new List<Category>();

            //Manually added the product to categories in the dropdown menu  
            foreach (var cat in thisProd.Categories)
            {
                someCategories.Add(cat.Category);
            }
            ViewBag.OtherCategories = allCategories.Except(someCategories).ToList();
            // ViewBag.prodWithCat=prodWithCat;

            return View("ProdDetail", mod);
        }
        [HttpPost("/products/{prodId}/relate")]
        public IActionResult AddCategoryAssociation(int prodId, ProdCatViewModel prodCatViewModel)
        {
            prodCatViewModel.nuAssociation.ProductId = prodId;
            db.Associations.Add(prodCatViewModel.nuAssociation);
            db.SaveChanges();

            return RedirectToAction("GetProduct", new { prodId = prodId });
        }
///////////////////////////////////////////////////////////////////////////////////
        [HttpGet("/categories/{catId}")]
        public IActionResult GetCategory(int catId)
        {
            Category thisCat = db.Categories.
            Include(category => category.Products)
            .ThenInclude(catProd => catProd.Product)
            .FirstOrDefault(c => c.CategoryId == catId);

            if (thisCat == null)
            {
                return RedirectToAction("Categories");
            }

            List<Product> unrelatedProducts = db.Products
            .Include(ProdCat => ProdCat.Categories).
            Where(ProdCat => ProdCat.Categories.Any(catProd => catProd.CategoryId == thisCat.CategoryId) == false).ToList();

            ProdCatViewModel mod = new ProdCatViewModel()
            {
                nuCategory = thisCat,
                prodViewModel = unrelatedProducts
            };
            List<Product> allProducts = db.Products.ToList();
            List<Product> someProducts = new List<Product>();

            //Manually added the product to categories in the dropdown menu  
            foreach (var prod in thisCat.Products)
            {
                someProducts.Add(prod.Product);
            }
            ViewBag.OtherProducts = allProducts.Except(someProducts).ToList();
            // ViewBag.prodWithCat=prodWithCat;

            return View("CatDetail", mod);
        }
        [HttpPost("/categories/{catId}/relate")]
        public IActionResult AddProductAssociation(int catId, ProdCatViewModel prodCatViewModel)
        {
            prodCatViewModel.nuAssociation.CategoryId = catId;
            db.Associations.Add(prodCatViewModel.nuAssociation);
            db.SaveChanges();

            return RedirectToAction("GetCategory", new { catId = catId });
        }
    }
}
