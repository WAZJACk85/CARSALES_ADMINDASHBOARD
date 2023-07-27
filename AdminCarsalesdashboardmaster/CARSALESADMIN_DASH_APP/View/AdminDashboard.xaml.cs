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
using System.Windows.Shapes;
using NewDesignTrial.View;
using NewDesignTrial.Models.DB;
using NewDesignTrial.Models;

namespace NewDesignTrial.View
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        //save Login Employee data for use in Update Personal details form

        CarEmployee emp;
        public AdminDashboard(CarEmployee emp)
        {
            InitializeComponent();       
            this.emp = emp;
        }


       
        private void buttonMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowStateBtnClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseBtnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            this.Hide();
            form.Show();
        }

     

        private void records_by_date_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new RentalsByDateUC());

        }

        private void records_by_customer_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new RentalsByCustomerUC());
        }

        

        private void add_customer_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new addCustomer());
        }
     

        private void addEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new addEmployee());

        }
        private void searchCustomer_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new SearchCustomerUC());
        }

        private void contactsBtn_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new ContactDetailsUC());
        }

        private void updateDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            //send Employee data to Update details window
            CarEmployee emp = this.emp;
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new UpdatePersonalDetailsUC(emp));
        }

        private void showAllCustomers_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new showCustomersUC());
        }

        private void showAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new EmployeesUC());
        }

        private void searchEmployeemenu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new SearchEmployeeUC());
        }

        private void show_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new ShowAvailableCars());
        }

        private void show_allcar_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new showAllCars());
        }

        private void rentCar_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new RentCar());
        }

        private void returnCar_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new ReturnCar());
        }

        private void new_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new AddCarUC());
        }

        private void reorder_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new ReorderCar());
        }

        private void update_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new SearchCar());
        }

        private void features_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new CarFeatures());
        }

        private void search_car_menu_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new SearchCarEmployee());

        }

        private void reportsBtn_Click(object sender, RoutedEventArgs e)
        {
            main_window_panel.Children.Clear();
            main_window_panel.Children.Add(new reportsUC());
        }
    }
}

   