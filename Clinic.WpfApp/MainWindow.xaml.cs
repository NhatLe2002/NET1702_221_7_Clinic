﻿using Clinic.WpfApp.UI.User;
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

namespace Clinic.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_wUserManagement_Click(object sender, RoutedEventArgs e)
        {
            var p = new wUserManagement();
            p.Owner = this;
            p.Show();
        }
        
        
    }
}