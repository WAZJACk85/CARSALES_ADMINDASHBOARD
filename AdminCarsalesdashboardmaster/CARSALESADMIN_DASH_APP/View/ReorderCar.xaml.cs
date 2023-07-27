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
    /// Interaction logic for ReorderCar.xaml
    /// </summary>
    public partial class ReorderCar : UserControl
    {
        public ReorderCar()
        {
// set data in the model combobox
            InitializeComponent();
            modelComboBox.ItemsSource = DAO.getModels();
            modelComboBox.DisplayMemberPath = "Model";
            //linked value to the combobox
            modelComboBox.SelectedValuePath = "Model";
        }
// add car button clicked
        private void addCarBtn_Click(object sender, RoutedEventArgs e)
        {
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
            string model = modelComboBox.Text;
// object of model and car
            CarModel tm = DAO.searchModelByName(model);
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

// car features
            CarFeature one = DAO.findFeatureById(1);
            CarFeature two = DAO.findFeatureById(2);
            CarFeature three = DAO.findFeatureById(3);
            CarFeature four = DAO.findFeatureById(4);
// create list of selected features
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

            try
            {
// add car to database with list of features
                DAO.reorderCar(it,Featurelist);
                MessageBox.Show("New Car added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
