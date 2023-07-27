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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewDesignTrial.Models;
using NewDesignTrial.Models.DB;

namespace NewDesignTrial.View
{
    /// <summary>
    /// Interaction logic for reportsUC.xaml
    /// </summary>
    public partial class reportsUC : UserControl
    {
        public reportsUC()
        {
            InitializeComponent();

     // add selection of car models into combobox to search rentals between specific dates
            modelComboBox.ItemsSource = DAO.getCarModelsPB();
            modelComboBox.DisplayMemberPath = "Model";
            modelComboBox.SelectedValuePath = "Model";

    // add selection of clients driving license into combobox to search rentals by customer
            licenseComboBox.ItemsSource = DAO.getCarCustomersPB();
            licenseComboBox.DisplayMemberPath = "LicenseNumber";
            licenseComboBox.SelectedValuePath = "LicenseNumber";
        }

        private void topFiveCarsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridCars.ItemsSource = DAO.getTopFiveCars();
                gridCars.Columns[0].Header = " Car ID ";
                gridCars.Columns[1].Header = " Registration";
                gridCars.Columns[2].Header = " Total rent";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void leastFiveModelsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridCars.ItemsSource = DAO.getLeastFiveRentedCarModels();
                gridCars.Columns[0].Header = " Model ID ";
                gridCars.Columns[1].Header = " Model";
                gridCars.Columns[2].Header = " Total rent";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saleBetweenDates_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string model = modelComboBox.Text;
                DateTime startDate = startDatePicker.SelectedDate.Value;
                DateTime endDate = endDatePicker.SelectedDate.Value;
                gridTotal.ItemsSource= DAO.getTotalPriceForSelectedCarModel(model, startDate, endDate);               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void rentalSaleForCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            string license = licenseComboBox.Text;
            try
            {
                gridCustomerTotal.ItemsSource = DAO.getTotalCustomerRentByLicense(license);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rentalfForSelectedMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int month = int.Parse(monthComboBox.Text);
                int year = int.Parse(yearTextBox.Text);
                gridRentalsTotal.ItemsSource = DAO.getTotalMonthlyRent(month, year);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
