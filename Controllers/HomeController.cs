using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using productsNcategoriescSharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace productsNcategoriescSharp.Controllers
{
    public class HomeController : Controller
    {
        private HomeContext dbContext;
     
        // here we can "inject" our context service into the constructorcopy
        public HomeController(HomeContext context)
        {
            dbContext = context;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> AllProducts = dbContext.Products
            .ToList();
            ViewBag.Products = AllProducts;
            return View();
        }

        [HttpPost("create/product")]
        public IActionResult CreateProduct(Product NewProduct)
        {
            Console.WriteLine(NewProduct.Name);
            if(ModelState.IsValid)
            {
                dbContext.Add(NewProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Product> AllProducts = dbContext.Products
            .ToList();
            ViewBag.Products = AllProducts;
            return View("Index");
        }

        [HttpGet("categories")]
        public IActionResult Categories()
        {
            List<Category> AllCategories = dbContext.Categories
            .ToList();
            ViewBag.Category = AllCategories;
            return View("Category");
        }

        [HttpPost("create/category")]
        public IActionResult CreateCategory(Category NewCategory)
        {
            Console.WriteLine(NewCategory.Name);
            if(ModelState.IsValid)
            {
                dbContext.Add(NewCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            List<Category> AllCategories = dbContext.Categories
            .ToList();
            ViewBag.Category = AllCategories;
            return View("Category");
        }

        [HttpGet("products/{id}")]
        public IActionResult UniqueProduct(int id)
        {
            var ProductToCategory = dbContext.Products
            .Include(product => product.Associations)
            .ThenInclude(association => association.Category).FirstOrDefault( prod => prod.ProductId == id);

            List<Category> AllCategories = dbContext.Categories
            .Include(category => category.Associations)
            .Where(cat => cat.Associations.All(association => association.ProductId != id)).ToList();
            ViewBag.Id = id;
            ViewBag.AllCategories = AllCategories;
            return View("UniqueProduct",ProductToCategory);
        }

        [HttpPost("addcategory")]
        public IActionResult AddCatToProd(Association newAss)
        {
            Console.WriteLine("in create category to product");
            Console.WriteLine(newAss.ProductId);
            if(ModelState.IsValid)
            {
            Product thisProduct = dbContext.Products
            .FirstOrDefault(prod => prod.ProductId == newAss.ProductId);
            Category thisCategory = dbContext.Categories
            .FirstOrDefault(cat => cat.CategoryId == newAss.CategoryId);
            Association thisAssociation = new Association();
            thisAssociation.CategoryId = newAss.CategoryId;
            thisAssociation.ProductId = newAss.ProductId;
            thisAssociation.Product = thisProduct;
            thisAssociation.Category = thisCategory;
            dbContext.Add(thisAssociation);
            dbContext.SaveChanges();
            return Redirect($"products/{thisProduct.ProductId}");
            }
            return View("UniqueProduct");

        }

        [HttpGet("category/{id}")]
        public IActionResult UniqueCategory(int id)
        {
            var CategoryToProduct = dbContext.Categories
            .Include(cat => cat.Associations)
            .ThenInclude(association => association.Product).FirstOrDefault( prod => prod.CategoryId == id);

            List<Product> AllProducts = dbContext.Products
            .Include(prod => prod.Associations)
            .Where(prod => prod.Associations.All(association => association.CategoryId != id)).ToList();
            ViewBag.Id = id;
            ViewBag.AllProducts = AllProducts;
            return View("UniqueCategory",CategoryToProduct);
        }

        [HttpPost("addproduct")]
        public IActionResult AddProdToCat(Association newAss)
        {
            Console.WriteLine(newAss.AssociationId);
            Console.WriteLine(newAss.ProductId);
            if(ModelState.IsValid)
            {
            Product thisProduct = dbContext.Products
            .FirstOrDefault(prod => prod.ProductId == newAss.ProductId);
            Category thisCategory = dbContext.Categories
            .FirstOrDefault(cat => cat.CategoryId == newAss.CategoryId);
            Association thisAssociation = new Association();
            thisAssociation.CategoryId = newAss.CategoryId;
            thisAssociation.ProductId = newAss.ProductId;
            thisAssociation.Product = thisProduct;
            thisAssociation.Category = thisCategory;
            dbContext.Add(thisAssociation);
            dbContext.SaveChanges();
            return Redirect($"category/{thisProduct.ProductId}");
            }
            return View("UniqueProduct");

        }

    }
}
