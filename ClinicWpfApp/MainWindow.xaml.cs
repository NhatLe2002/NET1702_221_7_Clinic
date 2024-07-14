using ClinicBusiness;
using ClinicData.Models;
using ClinicWpfApp.UI.ClinicPage;
using System.Net;
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

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTmp = -1;
                int.TryParse(txtClinicCode.Text, out idTmp);

                var item = await _business.GetById(txtClinicCode.Text);

                if (item.Data == null)
                {
                    var clinic = new Clinic()
                    {
                        ClinicName = txtClinicName.Text,
                        Address = txtAddress.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text,
                        ClinicImage = txtClinicImage.Text,
                        ClinicDescription = txtClinicDescription.Text,
                    };

                    var result = await _business.Save(clinic);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var clinic = item.Data as Clinic;
                    //currency.CurrencyCode = txtCurrencyCode.Text;

                    clinic.ClinicName = txtClinicName.Text;
                    clinic.Address = txtAddress.Text;
                    clinic.Phone = txtPhone.Text;
                    clinic.Email = txtEmail.Text;
                    clinic.ClinicImage = txtClinicImage.Text;
                    clinic.ClinicDescription = txtClinicDescription.Text;

                    var result = await _business.Update(clinic);
                    MessageBox.Show(result.Message, "Update");
                }

                txtClinicCode.Text = string.Empty;
                txtClinicName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtClinicImage.Text = string.Empty;
                txtClinicDescription.Text = string.Empty;
                this.LoadGrdCurrencies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
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
