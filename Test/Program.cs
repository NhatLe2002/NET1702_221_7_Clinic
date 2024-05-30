using ClinicBusiness;
using ClinicData.Models;

namespace Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var check = new ClinicBusinessClass();
            var list = await check.GetAll();

            /*foreach (var item in list)
            {
                if (item.Data is Clinic clinic)
                {
                    Console.WriteLine($"Clinic ID: {clinic.ClinicId}");
                    Console.WriteLine($"Clinic Name: {clinic.ClinicName}");
                    Console.WriteLine($"Address: {clinic.Address}");
                    Console.WriteLine($"Phone: {clinic.Phone}");
                    Console.WriteLine($"Email: {clinic.Email}");
                    Console.WriteLine($"Description: {clinic.ClinicDescription}");
                    Console.WriteLine("----");
                }
                else
                {
                    Console.WriteLine($"Status: {item.Status}, Message: {item.Message}");
                }
            }*/


            Console.WriteLine("----------");

            var checkcustomer = new CustomerBusiness();
            var customerlist = await checkcustomer.GetAllAsync();

            foreach (var item in customerlist)
            {
                if (item.Data is Customer customeritem)
                {
                    Console.WriteLine(customeritem.CustomerId);
                }
                else
                {
                    Console.WriteLine($"Status: {item.Status}, Message: {item.Message}");
                }
            }

        }
    }
}
