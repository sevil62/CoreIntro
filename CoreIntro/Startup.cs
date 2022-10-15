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
            //Hangi servisin kullanýlacagý burada öncelikle yazýlmak durumundadýr...(Ancak dikkat edin burada servisin sadece kullanýlacagý yazýlýr yani burada kullanýlmaz)..Burada sadece servisleri eklersiniz...

            //Burada standart bir Sql baglantýsýný belirlemek istiyorsanýz (sýnýf icerisinde optionBuilder'dan belirlemektense bu tercih edilir) burada belirlemelisiniz...


            //Pool kullanmak bir singletonPattern görevi görür...

            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
            //Yukarýdaki ifadede dikkat ederseniz UseLazyLoadingProxies ifadesi kullanýlmýstýr...Bu durum,.Net Core'daki Lazy Loading'in sürekli tetiklenebilmesi adýna Environment'inizi garanti altýna almanýzý saglar...

            //Authentication kimligi temsil eder

            //Dikkat edin Authentication iþlemini yapabilmek icin servisi burada böyle yaratmanýz gerekir. Yoksa Authentication iþlemleri çalýþmaz
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Home/Login";
            });
            //Authorization ilgili kimliðin bir þey yapmaya yetkisi var mý diye bakar

            //Session kullanacak iseniz(yarattýgýnýz Session sýnýfýnýn görevini yapabilmesini istiyorsanýz) ayarlamalarýnýzý burada eklemeyi sakýn unutmayýn..
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
            //Authentication'i Authorization'dan önce vermeye özen gösterin
            app.UseAuthentication();//kullanýcý kim bunu algýla demektir...
            app.UseAuthorization();//sizin yetkiniz var mý yok mu gibi durumlarda calýsacak bir metottur...

            //Sessione'u middleWare'e ekledik ama daha kullanmadýk o yüzden onu da kullanmalýsýnýz...
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
