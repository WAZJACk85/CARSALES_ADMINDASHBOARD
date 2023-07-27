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
    /// Interaction logic for SearchCarEmployee.xaml
    /// </summary>
    public partial class SearchCarEmployee : UserControl
    {
        public SearchCarEmployee()
        {
            InitializeComponent();
            registrationComboBox.ItemsSource = DAO.getAllCars();
            registrationComboBox.DisplayMemberPath = "RegistrationNumber";
            //linked value to the combobox
            registrationComboBox.SelectedValuePath = "RegistrationNumber";
        }
        private void showCarBtn_Click(object sender, RoutedEventArgs e)
        {
            string carRego = registrationComboBox.Text;
            if (carRego.Length == 0)
            {
                MessageBox.Show("Please choose car registration number");
                return;
            }
            else
            {
                IndividualCar car = DAO.findCarByRego(carRego);

                if (car != null)
                {
                    autoGrid.Visibility = Visibility.Visible;

                    modelTextBox.Text = car.CarModel.Model;
                    manufacturerTextBox.Text = car.CarModel.Manufacturer;
                    sizeComboBox.Text = car.CarModel.Size;
                    numberOfSeatsTextBox.Text = car.CarModel.Seats.ToString();
                    numberOfDoorsTextBox.Text = car.CarModel.Doors.ToString();
                    colorTextBox.Text = car.Colour;
                    WOFExpiryDatePicker.Text = car.WofexpiryDate.ToString();
                    REGOExpiryDatePicker.Text = car.RegistrationExpiryDate.ToString();
                    yearOfManufactureTextBox.Text = car.ManufactureYear.ToString();
                    dateImportedDatePicker.Text = car.DateImported.ToString();
                    statusComboBox.Text = car.Status;
                    fuelTypeComboBox.Text = car.FuelType;
                    transmissionComboBox.Text = car.Transmission;
                    depositPriceTextBox.Text = string.Format("{0:F2}", car.AdvanceDepositRequired);
                    rentalPriceTextBox.Text = string.Format("{0:F2}", car.DailyRentalPrice);
                    int carId = car.CarId;
                    List<CarFeature> features = DAO.getCarFeaturesById(carId);
                    if (features.Count != 0)
                    {
                        featureList.ItemsSource = features;
                        featureList.Visibility = Visibility.Visible;
                        extraFeaturesLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("No data found");
                        return;
                    }
                }
            }
        }

        private void registrationComboBox_DropDownClosed(object sender, EventArgs e)
        {
            autoGrid.Visibility = Visibility.Collapsed;
        }
    }
}
