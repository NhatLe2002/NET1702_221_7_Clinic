using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicRazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            // Add DbContext with connection string from appsettings.json
            builder.Services.AddDbContext<NET1702_PRN221_ClinicContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NET1702_PRN221_Clinic")));
            builder.Services.AddScoped<IClinicBusinessClass, ClinicBusinessClass>();
            
            //Add Dependency Injection
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddScoped<ICustomerBusinessClass, CustomerBusiness>();
            builder.Services.AddScoped<IUserBusiness, UserBusiness>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}