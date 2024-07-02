using ClinicBusiness;
using ClinicData.Models;
using ClinicWpfApp.UI.ClinicPage;
using ClinicWpfApp.UI.CustomerPage;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinicWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClinicBusinessClass _business;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Open_wCustomer_Click(object sender, RoutedEventArgs e)
        {
            var p = new wCustomer();
            p.Owner = this;
            p.Show();
        }

        private async void Open_wClinic_Click(object sender, RoutedEventArgs e)
        {
            var p = new wClinic();
            p.Owner = this;
            p.Show();
        }
    }
}
