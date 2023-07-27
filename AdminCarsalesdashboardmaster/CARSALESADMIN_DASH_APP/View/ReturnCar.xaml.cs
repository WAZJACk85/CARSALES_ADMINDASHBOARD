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
using NewDesignTrial.Models.DB;
using NewDesignTrial.Models;

namespace NewDesignTrial.View
{
    /// <summary>
    /// Interaction logic for ReturnCar.xaml
    /// </summary>
    public partial class ReturnCar : UserControl
    {
        public ReturnCar()
        {
// set registartion numbers data of rented cars in combobox
            InitializeComponent();
            registrationComboBox.ItemsSource = DAO.getRentedCars();
            registrationComboBox.DisplayMemberPath = "RegistrationNumber";
            registrationComboBox.SelectedValuePath = "RegistrationNumber";
        }
// set reurn date to today
        private void retunrCarWindow_Loaded(object sender, RoutedEventArgs e)
        {
            returnedDatePicker.SelectedDate = DateTime.Today;
        }

 // set data of the selected car    
        private void registrationComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string rego = registrationComboBox.Text;
            if(rego!=null)
            {
                try
                {
                    CarRental rentalRecord = DAO.findRentalRecordByCarRego(rego);
                    if (rentalRecord != null)
                    {
                        rentDayDatePicker.SelectedDate = rentalRecord.RentDate;
                        dueBackDatePicker.SelectedDate = rentalRecord.ReturnDueDate;
                        totalTextBox.Text = rentalRecord.TotalPrice.ToString();


//check if rental not over due

                        DateTime dueDate = dueBackDatePicker.SelectedDate.Value;
                        DateTime returnDate = returnedDatePicker.SelectedDate.Value;
                        if (returnDate > dueDate)
                        {
                            int days = (int)(returnDate - dueDate).TotalDays;
                            decimal price = rentalRecord.Car.DailyRentalPrice;
                            decimal totalPrice = days * price;

                            totalTextBox.Text = totalPrice.ToString();
                            MessageBox.Show("Total rent price has changed due to late return. Price to pay: " + totalPrice.ToString());
                            rentalRecord.ReturnDate = returnedDatePicker.SelectedDate.Value;

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
// return car button clicked
        private void returnCarBtn_Click(object sender, RoutedEventArgs e)
        {
            string rego = registrationComboBox.Text;
            CarRental rentalRecord = DAO.findRentalRecordByCarRego(rego);
            rentalRecord.ReturnDate = returnedDatePicker.SelectedDate.Value;
            rentalRecord.TotalPrice= Decimal.Parse(totalTextBox.Text);
                       

            try
            {
                DAO.returnCar(rentalRecord);
                MessageBox.Show("Car returned sucessfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void returnedDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime returnDate = returnedDatePicker.SelectedDate.Value;
            if(returnDate<DateTime.Today)
            {
                MessageBox.Show("Please sellect correct date");
                return;
            } 
            

        }
    }
}
