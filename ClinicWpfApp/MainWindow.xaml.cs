using ClinicBusiness;
using ClinicData.Models;
using ClinicWpfApp.UI.ClinicPage;
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
            _business = new ClinicBusinessClass();
            InitializeComponent();
            this.LoadGrdCurrencies();
        }
        private async void Open_wClinic_Click(object sender, RoutedEventArgs e)
        {
            var p = new wClinic();
            p.Owner = this;
            p.Show();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grdClinic_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grdClinic_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void LoadGrdCurrencies()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdClinic.ItemsSource = result.Data as List<Clinic>;
            }
            else
            {
                grdClinic.ItemsSource = new List<Clinic>();
            }
        }
    }
}