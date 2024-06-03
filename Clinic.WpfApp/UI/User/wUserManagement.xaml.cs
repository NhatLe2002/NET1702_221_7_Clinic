using ClinicBusiness;
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

namespace Clinic.WpfApp.UI.User
{
    /// <summary>
    /// Interaction logic for wUserManagement.xaml
    /// </summary>
    public partial class wUserManagement : Window
    {
        private readonly UserBusiness _business;
        public wUserManagement()
        {
            InitializeComponent();
        }
        private async void Open_wUserManagement_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
        }
        //private async void LoadGrdCurrencies()
        //{
        //    var result = await _business.GetAll();

        //    if (result.Status > 0 && result.Data != null)
        //    {
        //        grdCurrency.ItemsSource = result.Data as List<User>;
        //    }
        //    else
        //    {
        //        grdCurrency.ItemsSource = new List<User>();
        //    }
        //}
    }
}
