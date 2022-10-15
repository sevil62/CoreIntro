using CoreIntro.Models.Context;
using CoreIntro.Models.Entites;
using CoreIntro.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreIntro.Controllers
{
    public class CategoryController : Controller
    {
        MyContext _db;
        public CategoryController(MyContext db)
        {
            _db = db;
        }

        public IActionResult CategoryList()
        {
            CategoryVM cvm=new CategoryVM()
            {
                Categories=_db.Categories.ToList()
            };
            
            return View(cvm);
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        public IActionResult UpdateCategory(int id )
        {
            CategoryVM cvm = new CategoryVM()
            {
                Category = _db.Categories.Find(id)
            };
            return View();
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            Category toBeUpdated = _db.Categories.Find(category.ID);
            toBeUpdated.CategoryName = category.CategoryName;
            toBeUpdated.Discription = category.Discription;
            _db.SaveChanges();
            return RedirectToAction("CategoryList");

        }
        public IActionResult DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}
