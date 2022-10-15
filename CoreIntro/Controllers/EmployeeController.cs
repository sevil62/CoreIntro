using CoreIntro.Models.Context;
using CoreIntro.Models.Entites;
using CoreIntro.VMClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreIntro.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        MyContext _db;
        public EmployeeController(MyContext db)
        {
            _db = db;
        }

        public IActionResult EmployeeList()
        {
            EmployeeVM evm = new EmployeeVM()
            {
                Employees = _db.Employees.ToList()
            };
            return View(evm);
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
           _db.Employees.Add(employee);
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");

        }
        public IActionResult UpdateEmployee(int id)
        {
            EmployeeVM evm = new EmployeeVM()
            {
                Employee = _db.Employees.Find(id)
            };
            return View(evm);
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            Employee toBeUpdated = _db.Employees.Find(employee.ID);
            toBeUpdated.FirstName = employee.FirstName;
            toBeUpdated.Role= employee.Role;
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        public IActionResult DeleteEmployee(int id)
        {
            _db.Employees.Remove(_db.Employees.Find(id));
            _db.SaveChanges();
            return RedirectToAction("EmployeeList");
            
        }
    }
}
