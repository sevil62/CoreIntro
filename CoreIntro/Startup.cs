using CoreIntro.Models.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreIntro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Hangi servisin kullan�lacag� burada �ncelikle yaz�lmak durumundad�r...(Ancak dikkat edin burada servisin sadece kullan�lacag� yaz�l�r yani burada kullan�lmaz)..Burada sadece servisleri eklersiniz...

            //Burada standart bir Sql baglant�s�n� belirlemek istiyorsan�z (s�n�f icerisinde optionBuilder'dan belirlemektense bu tercih edilir) burada belirlemelisiniz...


            //Pool kullanmak bir singletonPattern g�revi g�r�r...

            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
            //Yukar�daki ifadede dikkat ederseniz UseLazyLoadingProxies ifadesi kullan�lm�st�r...Bu durum,.Net Core'daki Lazy Loading'in s�rekli tetiklenebilmesi ad�na Environment'inizi garanti alt�na alman�z� saglar...

            //Authentication kimligi temsil eder

            //Dikkat edin Authentication i�lemini yapabilmek icin servisi burada b�yle yaratman�z gerekir. Yoksa Authentication i�lemleri �al��maz
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Home/Login";
            });
            //Authorization ilgili kimli�in bir �ey yapmaya yetkisi var m� diye bakar

            //Session kullanacak iseniz(yaratt�g�n�z Session s�n�f�n�n g�revini yapabilmesini istiyorsan�z) ayarlamalar�n�z� burada eklemeyi sak�n unutmay�n..
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20);
                x.Cookie.HttpOnly = true;
                x.Cookie.IsEssential = true;

            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //Authentication'i Authorization'dan �nce vermeye �zen g�sterin
            app.UseAuthentication();//kullan�c� kim bunu alg�la demektir...
            app.UseAuthorization();//sizin yetkiniz var m� yok mu gibi durumlarda cal�sacak bir metottur...

            //Sessione'u middleWare'e ekledik ama daha kullanmad�k o y�zden onu da kullanmal�s�n�z...
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=EmployeeList}/{id?}");
            });
        }
    }
}
