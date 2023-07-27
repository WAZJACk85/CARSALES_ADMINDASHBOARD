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
//using System.Data.Entity.Infrastructure;
// using System.Data.Entity;
using NewDesignTrial.Utility;

namespace NewDesignTrial.View
{
    /// <summary>
    /// Interaction logic for AddCarUC.xaml
    /// </summary>
    public partial class AddCarUC : UserControl
    {
        public AddCarUC()
        {
            InitializeComponent();
        }
// Add Car button clicked 
// No data validation added.
        private void addCarBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string model = modelTextBox.Text;
                string manufacturer = manufacturerTextBox.Text;
                string size = sizeComboBox.Text;
                int seats = int.Parse(numberOfSeatsTextBox.Text);
                int doors = int.Parse(numberOfDoorsTextBox.Text);
                string color = colorTextBox.Text;
                string rego = regoTextBox.Text;
                DateTime WOFexpiry = DateTime.Parse(WOFExpiryDatePicker.Text);
                DateTime RegoExpiry = DateTime.Parse(REGOExpiryDatePicker.Text);
                DateTime dateImported = DateTime.Parse(dateImportedDatePicker.Text);
                int manufactureYear = int.Parse(yearOfManufactureTextBox.Text);
                string status = statusComboBox.Text;
                string fuel = fuelTypeComboBox.Text;
                string transmission = transmissionComboBox.Text;
                decimal rentPrice = decimal.Parse(rentalPriceTextBox.Text);
                decimal deposit = decimal.Parse(depositPriceTextBox.Text);

// create object of Car Model
                CarModel tm = new CarModel();
                tm.Model = model;
                tm.Manufacturer = manufacturer;
                tm.Size = size;
                tm.Seats = seats;
                tm.Doors = doors;
// create object of Individual Car
                IndividualCar it = new IndividualCar();
                it.Colour = color;
                it.RegistrationNumber = rego;
                it.WofexpiryDate = WOFexpiry;
                it.RegistrationExpiryDate = RegoExpiry;
                it.DateImported = dateImported;
                it.ManufactureYear = manufactureYear;
                it.Status = status;
                it.FuelType = fuel;
                it.Transmission = transmission;
                it.DailyRentalPrice = rentPrice;
                it.AdvanceDepositRequired = deposit;
                it.CarModel = tm;

//Get main features
                    CarFeature one = DAO.findFeatureById(1);                
                    CarFeature two = DAO.findFeatureById(2);
                    CarFeature three = DAO.findFeatureById(3);
                    CarFeature four = DAO.findFeatureById(4);

// create list of features
                    List<CarFeature> Featurelist = new List<CarFeature>();


                    if ((bool)airConCheckBox.IsChecked)
                    {
                        if (one != null)
                        {
                            Featurelist.Add(one);
                        }
                    }
                    else if ((bool)rearDoorCheckBox.IsChecked)
                    {
                        if (two != null)
                        {
                            Featurelist.Add(two);
                        }
                    }
                    else if ((bool)alarmCheckBox.IsChecked)
                    {
                        if (three != null)
                        {
                            Featurelist.Add(three);
                        }
                    }
                    if ((bool)keylessCheckBox.IsChecked)
                    {
                        if (four != null)
                        {
                            Featurelist.Add(four);
                        }
                    }
// add car including list of features

                    DAO.addCar(it, Featurelist);
                    MessageBox.Show("New Car added successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }

