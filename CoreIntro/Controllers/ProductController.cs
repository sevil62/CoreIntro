using CoreIntro.Models.Context;
using CoreIntro.Models.Entites;
using CoreIntro.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreIntro.Controllers
{
    public class ProductController : Controller
    {
        MyContext _db;
        public ProductController(MyContext db)
        {
            _db = db;
        }
    
        public IActionResult ListProduct()
        {
            ProductVM pvm = new ProductVM()
            {
                Products = _db.Products.ToList(),
                Categories = _db.Categories.ToList(),
            };
            return View(pvm);
        }
        public IActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM()
            {
                
                Categories = _db.Categories.ToList(),
            };
            return View(pvm);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {

            _db.Products.Add(product);
            _db.SaveChanges();
            return  RedirectToAction("ListProduct");
        }
        public IActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM()
            {
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            Product toBeUpdated = _db.Products.Find(product.ID);
            toBeUpdated.CategoryID=product.CategoryID;
            toBeUpdated.ProductName = product.ProductName;
            toBeUpdated.UnitsInStock=product.UnitsInStock;
            _db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
        public IActionResult DeleteProduct(int id)
        {
            _db.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return RedirectToAction("ListProduct");
        }
    }
}
