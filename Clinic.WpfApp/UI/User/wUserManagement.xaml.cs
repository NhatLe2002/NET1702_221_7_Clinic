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
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtCurrencyCode.Text, out idTmp);

                var item = await _business.GetById(txtCurrencyCode.Text);

                if (item.Data == null)
                {
                    var currency = new Currency()
                    {
                        CurrencyCode = txtCurrencyCode.Text,
                        CurrencyName = txtCurrencyName.Text,
                        NationCode = txtNationCode.Text,
                        IsActive = chkIsActive.IsChecked
                    };

                    var result = await _business.Create(currency);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var currency = item.Data as Currency;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    currency.CurrencyName = txtCurrencyName.Text;
                    currency.NationCode = txtNationCode.Text;
                    currency.IsActive = chkIsActive.IsChecked;

                    var result = await _business.Update(currency);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCurrencyCode.Text = string.Empty;
                txtCurrencyName.Text = string.Empty;
                txtNationCode.Text = string.Empty;
                chkIsActive.IsChecked = false;
                this.LoadGrdCurrencies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }
        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void LoadGrdCurrencies()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCurrency.ItemsSource = result.Data as List<User>;
            }
            else
            {
               grdCurrency.ItemsSource = new List<User>();
            }
        }
    }
}
