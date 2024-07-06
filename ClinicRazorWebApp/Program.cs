using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;

namespace ClinicRazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();
            //Add Session
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IClinicBusinessClass, ClinicBusinessClass>();

            //Add Dependency Injection
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddScoped<ICustomerBusinessClass, CustomerBusiness>();

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

            // Add middleware session into pipeline
            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}