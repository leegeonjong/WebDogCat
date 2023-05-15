using dogcat.Data;
using Microsoft.EntityFrameworkCore;

namespace dogcat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages().AddSessionStateTempDataProvider();

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

            builder.Services.AddDbContext<DogcatDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbName"))
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}