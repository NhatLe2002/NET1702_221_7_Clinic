using ClinicBusiness;
using ClinicBusiness.Base;
namespace Test1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Đặt lại tên khác để nó hiểu (ClinicBusiness)
            var clinicBusiness = new ClinicBusiness.ClinicBusiness();
            var currencyList =  clinicBusiness.GetAllAsync();

            Console.WriteLine("Hello, World!");
            foreach (var item in currencyList)
            {
                Console.WriteLine($"Status: {item.Status}, Message: {item.Message}");
            }
        }
    }
}
