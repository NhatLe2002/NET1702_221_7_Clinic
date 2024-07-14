using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;

namespace Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var check = new ClinicBusinessClass();
           /* var data = await check.GetById("1");
            if (data.Status == 1 && data.Data is Clinic clinic)
            {
                Console.WriteLine(clinic.ClinicId);
                Console.WriteLine(clinic.Address);
            }*/


            /* var delte = await check.DeleteById("13");
             Console.WriteLine(delte.Data);
 */
            var checklistfilter = check.GetAllByFillterAndPagingAsync("Phòng khám vipp", "Sì Gòn", "0355123456", 1, 2, 1, 10);


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
