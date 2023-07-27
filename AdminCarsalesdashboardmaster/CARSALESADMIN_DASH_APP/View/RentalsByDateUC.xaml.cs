﻿using System;
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
    /// Interaction logic for RentalsByDateUC.xaml
    /// </summary>
    public partial class RentalsByDateUC : UserControl
    {
        public RentalsByDateUC()
        {
            InitializeComponent();
        }
// show button clicked
        private void showRentalsBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DateTime startDate = startDatePicker.SelectedDate.Value;
                DateTime endDate = endDatePicker.SelectedDate.Value;
// get list of rentals
                List<CarRentalsWithCustomerName> rentals=DAO.getRentalsBetweenDates(startDate, endDate);
                if (rentals.Count() > 0)
                {
                    carRentalsDataGrid.ItemsSource = rentals;
                    carRentalsDataGrid.Columns[0].Header = " Rent ID ";
                    carRentalsDataGrid.Columns[1].Header = " Car";
                    carRentalsDataGrid.Columns[2].Header = "Customer";
                    carRentalsDataGrid.Columns[3].Header = "Rent Date";
                    carRentalsDataGrid.Columns[4].Header = "Return Due";
                    carRentalsDataGrid.Columns[5].Header = "Day Returned";
                    carRentalsDataGrid.Columns[6].Header = "Total Price";
 
                }
                else
                {
                    MessageBox.Show("No records found");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
