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

namespace NewDesignTrial.View
{
    /// <summary>
    /// Interaction logic for showAllCars.xaml
    /// </summary>
    public partial class showAllCars : UserControl
    {
        public showAllCars()
        {
            InitializeComponent();
        }

        private void AllCars_Loaded(object sender, RoutedEventArgs e)
        {
            gridCars.ItemsSource = DAO.getAllCars();
            gridCars.Columns[0].Header = " ID";
            gridCars.Columns[1].Header = "Reg.no";
            gridCars.Columns[2].Header = "Model";
            gridCars.Columns[3].Header = "Size";
            gridCars.Columns[4].Header = "WOF expiry";
            gridCars.Columns[5].Header = "REGO expiry";
            gridCars.Columns[6].Header = "Build Year";
            gridCars.Columns[7].Header = "  Fuel";
            gridCars.Columns[8].Header = "Transmission";
            gridCars.Columns[9].Header = "  Rent";
            gridCars.Columns[10].Header = "Deposit";
            gridCars.Columns[11].Header = "Status";
        }
    }
}
