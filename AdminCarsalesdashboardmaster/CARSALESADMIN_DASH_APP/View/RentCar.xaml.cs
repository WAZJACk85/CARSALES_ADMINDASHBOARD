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
    /// Interaction logic for RentCar.xaml
    /// </summary>
    public partial class RentCar : UserControl
    {
        public RentCar()
        {
// set data in customers name combobox and in car registration combobox
            InitializeComponent();
            nameComboBox.ItemsSource = DAO.getCustomers();
            nameComboBox.DisplayMemberPath = "Name";
            registrationComboBox.ItemsSource = DAO.getAvailableCars();
            registrationComboBox.DisplayMemberPath = "RegistrationNumber";


            //linked value to the combobox
            nameComboBox.SelectedValuePath = "Name";
            registrationComboBox.SelectedValuePath = "RegistrationNumber";
        }
// if customer name was choosen, combobox will display license number (in case if there more than 1 customer with same name)
        private void nameComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string customerName = nameComboBox.Text;
            licenseComboBox.ItemsSource = DAO.findCustomersByName(customerName);
            licenseComboBox.DisplayMemberPath = "LicenseNumber";
            licenseComboBox.SelectedValuePath = "LicenseNumber";
        }
// set rent day to today
        private void rentCarWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rentDayDatePicker.SelectedDate = DateTime.Today;
            
        }
// set data of the selected car
        private void registrationComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string registration = registrationComboBox.Text;
            if(registration !=null)
            {
                
                string deposit = string.Format("{0:F2}", DAO.findCarByRego(registration).AdvanceDepositRequired); DepositTextBox.Text = deposit;
                //----------> for now, working in REG but must look into this break                
            }
            
        }

        private void rentDayDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rentDayDatePicker.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Please select correct day");
                return;
            }
        }
// chek if due back day correct
        private void dueBackDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dueBackDatePicker.SelectedDate < DateTime.Today && dueBackDatePicker.SelectedDate < rentDayDatePicker.SelectedDate)
            {
                MessageBox.Show("Please select correct day");
                return;
            }
            else
            {
                string registration = registrationComboBox.Text;
                double price = (double)DAO.findCarByRego(registration).DailyRentalPrice;
                DateTime rentDate = DateTime.Parse(rentDayDatePicker.SelectedDate.ToString());
                DateTime rentDue = DateTime.Parse(dueBackDatePicker.SelectedDate.ToString());
                int days = (int)(rentDue - rentDate).TotalDays;
                if(rentDue==rentDate)
                {
                    days = 1;
                }
                double totalPrice = days * price;

                TotalPriceTextBox.Text =totalPrice.ToString();
            }
        }
// rent car button pressed
        private void rentCarBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string registration = registrationComboBox.Text;
            string license = licenseComboBox.Text;

            int carId = DAO.findCarByRego(registration).CarId;
            int CustomerID = DAO.findCustomerByLicenseNumber(license).CustomerId;
            DateTime rentDate = DateTime.Parse(rentDayDatePicker.SelectedDate.ToString());
            DateTime rentDue = DateTime.Parse(dueBackDatePicker.SelectedDate.ToString());
            decimal totalPrice = decimal.Parse(TotalPriceTextBox.Text);

// create object of car rental
            CarRental tr = new CarRental();
            tr.CarId = carId;
            tr.CustomerId=CustomerID;
            tr.RentDate=rentDate;
            tr.ReturnDueDate=rentDue;
            tr.TotalPrice=totalPrice;
            

            try
            {
                DAO.rentCar(tr, carId);               
                MessageBox.Show("Car rented succesfully");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }
    }
}
