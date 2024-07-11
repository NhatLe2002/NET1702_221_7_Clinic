using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using ClinicData.Repository;

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
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            //Add Dependency Injection
            builder.Services.AddScoped<ICommonService, CommonService>();
            builder.Services.AddScoped<ICustomerBusinessClass, CustomerBusiness>();
            builder.Services.AddScoped<IUserBusinessClass, UserBusiness>();
            builder.Services.AddScoped<IClinicBusinessClass, ClinicBusinessClass>();
            builder.Services.AddScoped<IAppointmentBusinessClass, AppointmentBusinessClass>();
            //builder.Services.AddScoped<IAppointmentDetailBusiness, AppointmentDetailRepository>();

            builder.Services.AddScoped<IDentistBusiness, DentistBusinessClass>();

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