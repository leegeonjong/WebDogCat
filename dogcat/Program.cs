using dogcat.Data;
using dogcat.Repositories;
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

            builder.Services.AddSession();

            builder.Services.AddDbContext<DogcatDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbName"))
            );

            builder.Services.AddScoped<IPetRepositories, PetRepositories>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}