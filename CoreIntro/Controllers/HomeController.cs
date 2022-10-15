using CoreIntro.Models;
using CoreIntro.Models.Context;
using CoreIntro.Models.Entites;
using CoreIntro.VMClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreIntro.Controllers
{
    public class HomeController : Controller
    {
        MyContext _db;
        public HomeController(MyContext db)
        {
            _db = db;   
        }
        //.Net Core Authorization İşlemleri


        //Async metotlar her zaman generic bir Task döndürmek zorundadırlar...İsterseniz döndürülen Task'i kullanın isterseniz kullanmayın ama döndürmek zorundasınız.. Task class'ı asenkron metotların calısma prensipleri hakkında ayrıntılı bilgiyi tutan (Metot calısırken hata var mı, metodun bu görevi yapma sırasında kendisine eş zamanlı gelen istekler,metodun calısma durumunu(success,flawed) içerisinde saklayan bir yapıdır.. O yüzden normal şartlarda döndüreceğiniz değeri Task'e generic olarak vermek zorundasınız...
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            Employee loginEmployee = _db.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName);
            if (loginEmployee != null)
            {
                //Claim, rol bazlı veya identity bazlı güvenlik işlemlerinden sorumlu olan bir class'tır... Siz dilerseniz birden fazla Claim nesnesi yaratıp hepsini aynı anda kullanabilirsiniz...
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,loginEmployee.Role.ToString()),
                };
                ClaimsIdentity userIdentity=new ClaimsIdentity(claims,"login"); //burada login ismine sahip olan bir güvenlik durumu yarattık ve bu durum icin hangi güvenlik önlemlerinin calısacagını belirledik...

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); //.Net Core'un icerisinde artık paterniyle ve yontemiyle tamamen belirtilmiş olan security işlemlerinin artık tetiklenmesi lazım(yani login işleminin Authorization'a göre yapılabilmesi adına)

                //asenkron metotlar calıstıkları zaman baska bir işlemin engellenmemesini saglayan metotlardır...

                //Eger siz bir async metot cagırıyorsanız bu metoda await keyword'unu vermeniz gerekir ki amacı dahilinde calısabilsin... await keyword'unu sadece async bir metodu cagırırken ve async olarak tanımlanmıs bir metotta verebilirsiniz...
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("ListProduct", "Product");
            }
            return View(new EmployeeVM { Employee=employee});
        }
        public async Task<IActionResult>LogOut( )
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        
    }
}
