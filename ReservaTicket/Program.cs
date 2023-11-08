using Microsoft.EntityFrameworkCore;
using ReservaTicket.Context;
using Microsoft.AspNetCore.Session;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TicketeraDataBaseContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:TicketeraDBConnection"]));
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();
       

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            
        }

        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}