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

namespace ClinicWpfApp.UI.CustomerPage
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly ClinicBusinessClass _business;
        public wCustomer()
        {
            InitializeComponent();
            _business = new ClinicBusinessClass();
            LoadGrdCustomer();

            this.DataContext = this;
        }

        private async void LoadGrdCustomer()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

        private async void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            /*
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(currencyCode);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomer();
                }
            }
            */
        }

        private async void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            /*
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.CustomerId.ToString());

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Customer;
                            //txtCurrencyCode.Text = item.CurrencyCode;
                            //txtCurrencyName.Text = item.CurrencyName;
                            //txtNationCode.Text = item.NationCode;
                            //chkIsActive.IsChecked = item.IsActive;
                        }
                    }
                }
            }
            */
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
  /*          try
            {
                //int idTmp = -1;
                //int.TryParse(txtCurrencyCode.Text, out idTmp);

                var item = await _business.GetById(txtId.Text);

                if (item.Data == null)
                {
                    string selectedGender = "";
                    if (cbGender.SelectedItem != null)
                    {
                        ComboBoxItem selectedItem = (ComboBoxItem)cbGender.SelectedItem;
                        string selectedGender = selectedItem.Content.ToString();
                    }
                    var customer = new Customer()
                    {
                        Address = txtAddress.Text,
                        CustomerName = txtName.Text,
                        Email = txtEmail.Text,
                        Gender = selectedGender
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
  */
        }
    }
}
