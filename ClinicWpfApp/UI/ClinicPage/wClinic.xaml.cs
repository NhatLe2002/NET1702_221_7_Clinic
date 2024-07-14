using ClinicBusiness;
using ClinicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicWpfApp.UI.ClinicPage
{
    /// <summary>
    /// Interaction logic for wClinic.xaml
    /// </summary>
    public partial class wClinic : Window
    {
        private readonly ClinicBusinessClass _businessClinic;

        public wClinic()
        {
            _businessClinic = new ClinicBusinessClass();
            InitializeComponent();
            this.LoadGrdwClinic();
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

        private void Open_wClinic_Click(object sender, RoutedEventArgs e)
        {
            // Đây là nơi để viết chức năng của bạn khi click vào menu item
        }

        private async void LoadGrdwClinic()
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
