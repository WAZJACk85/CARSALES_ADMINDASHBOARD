using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewDesignTrial.Models.DB;

namespace NewDesignTrial.Models
{
    internal class DAO
    {

    //---------->Customers

    //----------> And new customer
        public static void addCustomer(CarCustomer cust)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.CarCustomers.Add(cust);
                ctx.SaveChanges();
            }
        }

    //----------> Get customer details including personal details
        public static List<CarCustomerWithName> getCustomers()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarCustomers.Include(p => p.Customer).Select(
                    te => new CarCustomerWithName()
                    {
                        CustomerId = te.CustomerId,
                        Name = te.Customer.Name,
                        Address = te.Customer.Address,
                        Telephone = te.Customer.Telephone,
                        LicenseNumber = te.LicenseNumber,
                        Age = te.Age,
                        LicenseExpiryDate = te.LicenseExpiryDate.ToString("dd/MM/yyyy"),

                    }).ToList();
            }
        }
    //----------> Find customer with personal details by his full name
        public static List<CarCustomerWithName> findCustomersByName(string name)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarCustomers.Include(p => p.Customer).Where(tc=>tc.Customer.Name==name).Select(
                    te => new CarCustomerWithName()
                    {
                        CustomerId = te.CustomerId,
                        Name = te.Customer.Name,
                        Address = te.Customer.Address,
                        Telephone = te.Customer.Telephone,
                        LicenseNumber = te.LicenseNumber,
                        Age = te.Age,
                        LicenseExpiryDate = te.LicenseExpiryDate.ToString("dd/MM/yyyy"),
                    }).ToList();
            }
        }

     //----------> Find customer by his id
        public static CarCustomer findCustomerById(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
                return ctx.CarCustomers.Include(p => p.Customer).Where(cust => cust.CustomerId == id).FirstOrDefault();

        }

    //----------> Find customer by his Driving License number
        public static CarCustomer findCustomerByLicenseNumber(string license)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
                return ctx.CarCustomers.Include(p => p.Customer).Where(cust => cust.LicenseNumber == license).FirstOrDefault();

        }

     //----------> Update Customer details
        public static void updateCustomer(CarCustomer cust, CarPerson tp)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(tp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.SaveChanges();
            }
        }


    //----------> Cars

    //----------> Add a new car including a new model
        public static void addCar(IndividualCar car,List<CarFeature> features)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.IndividualCars.Add(car);
                ctx.SaveChanges();
                int id = car.CarId;

                if(features != null)
                {
                    foreach (var feature in features)
                    {
                        car.Features.Add(feature);
                    }
                }
                ctx.SaveChanges();
            }
        }

    //----------> Add a new car with existing model ( reorder same model)
        public static void reorderCar(IndividualCar it, List<CarFeature> features)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.IndividualCars.Add(it);
                ctx.Entry(it.CarModel).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                ctx.SaveChanges();
                int id = it.CarId;

                if (features != null)
                {
                    foreach (var feature in features)
                    {
                        it.Features.Add(feature);
                    }
                }
                ctx.SaveChanges();
            }
        }

    //----------> Find car by ID
        public static IndividualCar findCarById(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.IndividualCars.Include(i => i.CarModel).Where(it => it.CarId == id).FirstOrDefault();
            }
        }

    //----------> Find car by registration number
        public static IndividualCar findCarByRego(string rego)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.IndividualCars.Include(i => i.CarModel).Where(it => it.RegistrationNumber == rego).FirstOrDefault();
            }
        }
    //----------> Update Car details
        public static void updateCar(IndividualCar car, CarModel model)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.Entry(car).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                ctx.SaveChanges();
            }
        }

       


        //----------> Find all cars
        public static List<CarsModelFeatures> getAllCars()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.IndividualCars.Include(i => i.CarModel).Select(
                    it => new CarsModelFeatures()
                    {
                        CarId = it.CarId,
                        RegistrationNumber = it.RegistrationNumber,
                        Model = it.CarModel.Model,
                        Size = it.CarModel.Size,
                        WofexpiryDate = it.WofexpiryDate.ToString("dd/MM/yyyy"),
                        RegistrationExpiryDate = it.RegistrationExpiryDate.ToString("dd/MM/yyyy"),
                        ManufactureYear = it.ManufactureYear,
                        FuelType = it.FuelType,
                        Transmission = it.Transmission,
                        DailyRentalPrice = string.Format("{0:F2}", it.DailyRentalPrice),
                        AdvanceDepositRequired = string.Format("{0:F2}", it.AdvanceDepositRequired),
                        Status = it.Status,
                    }).ToList();
            }
        }

     //----------> Find only available for rent cars

        public static List<CarsModelFeatures> getAvailableCars()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.IndividualCars.Include(i => i.CarModel).Where(st => st.Status == "Available to Buy").Select(
                    it => new CarsModelFeatures()  //-----> change to buy should reflect new db constraint
                    {
                        CarId = it.CarId,
                        RegistrationNumber = it.RegistrationNumber,
                        Model = it.CarModel.Model,
                        Size = it.CarModel.Size,
                        WofexpiryDate = it.WofexpiryDate.ToString("dd/MM/yyyy"),
                        RegistrationExpiryDate = it.RegistrationExpiryDate.ToString("dd/MM/yyyy"),
                        ManufactureYear = it.ManufactureYear,
                        FuelType = it.FuelType,
                        Transmission = it.Transmission,
                        DailyRentalPrice = string.Format("{0:F2}", it.DailyRentalPrice),
                        AdvanceDepositRequired = string.Format("{0:F2}", it.AdvanceDepositRequired),
                        Status = it.Status,
                    }).ToList();
            }
        }

    //----------> Find all rented out cars
        public static List<CarsModelFeatures> getRentedCars()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.IndividualCars.Include(i => i.CarModel).Where(st => st.Status == "rented").Select(
                    it => new CarsModelFeatures()
                    {
                        CarId = it.CarId,
                        RegistrationNumber = it.RegistrationNumber,
                        Model = it.CarModel.Model,
                        Size = it.CarModel.Size,
                        WofexpiryDate = it.WofexpiryDate.ToString("dd/MM/yyyy"),
                        RegistrationExpiryDate = it.RegistrationExpiryDate.ToString("dd/MM/yyyy"),
                        ManufactureYear = it.ManufactureYear,
                        FuelType = it.FuelType,
                        Transmission = it.Transmission,
                        DailyRentalPrice = string.Format("{0:F2}", it.DailyRentalPrice),
                        AdvanceDepositRequired = string.Format("{0:F2}", it.AdvanceDepositRequired),
                        Status = it.Status,
                    }).ToList();
            }
        }

     //----------> Change car status ( used for car renting)

        private static void changeCarStatus(int carId)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                IndividualCar it = ctx.IndividualCars.Where(it => it.CarId == carId).FirstOrDefault();
                if (it != null)
                {if(it.Status== "Available to Buy") //------> changed to buy as per new db constraint
                    {
                        it.Status = "rented";
                    }
                    else
                    {
                        it.Status = "Available to Buy"; //----->  changed to buy as db constraint new
                    }
                    
                    ctx.SaveChanges();
                }
            }
        }
       

        //----------> Car Models

        //----------> Get all car models
        public static List<CarModel> getModels()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarModels.ToList();
            }
        }

    //----------> Search Car model by model name
        public static CarModel searchModelByName(string name)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarModels.Where(tm => tm.Model == name).FirstOrDefault();
            }
        }

       

    //----------> Car Features
    //----------> Find Feature by ID
        public static CarFeature findFeatureById(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarFeatures.Where(tf => tf.FeatureId == id).FirstOrDefault();
            }
        }

    //----------> Find Feature by name
        public static CarFeature findFeatureByName(string feature)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarFeatures.Where(tf => tf.Description == feature).FirstOrDefault();
            }
        }
    //----------> Get list of features of the specific car by car ID
        public static List<CarFeature> getCarFeaturesById(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarFeatures.Where(i => i.Cars.Any(i => i.CarId == id)).ToList();
            }
        }

    //----------> Get all features
        public static List<CarFeature> getAllFeatures()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarFeatures.ToList();
            }
        }

    //----------> Add new feature to the feature list
        public static void addFeature(CarFeature feature)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.CarFeatures.Add(feature);
                ctx.SaveChanges();
            }
        }

    //----------> Add new feature to the car
        public static void addFeatureToCar (int id,CarFeature feature)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                var individualCar = ctx.IndividualCars.Find(id);
                individualCar.Features.Add(feature);
                ctx.SaveChanges();
            }
        }

    //---------->Employees
    //----------> Add a new employee
        public static void addEmployee(CarEmployee te)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.CarEmployees.Add(te);
                ctx.SaveChanges();
            }
        }

    //----------> Login validation ( used on the main window)
        public static CarEmployee loginValidation(string username, string password)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarEmployees.Include(p => p.Employee).Where(emp => emp.Username == username && emp.Password == password).FirstOrDefault();
            }
        }

    //---------->  Search contact details of any person in the system

        public static CarPerson searchContact(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarPeople.Where(p => p.PersonId == id).FirstOrDefault();
            }
        }
    //----------> Get all employees including their personal details
        public static List<CarEmployeeWithName> getEmployees()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarEmployees.Include(p => p.Employee).Select(
                    te => new CarEmployeeWithName()
                    {
                        EmployeeId = te.EmployeeId,
                        Name = te.Employee.Name,
                        Address = te.Employee.Address,
                        Role = te.Role,
                        Telephone = te.Employee.Telephone,
                        OfficeAddress = te.OfficeAddress,
                        PhoneExtensionNumber = te.PhoneExtensionNumber,
                        Username = te.Username,
                        Password = te.Password,                      
                    }).ToList();
            }
        }

    //----------> Find employee by his ID
        public static CarEmployee findEmployeeById(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
                return ctx.CarEmployees.Include(p => p.Employee).Where(emp => emp.EmployeeId == id).FirstOrDefault();

        }

    //----------> Find employee by his full name
        public static CarEmployee findEmployeeByName(string name)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
                return ctx.CarEmployees.Include(p => p.Employee).Where(emp => emp.Employee.Name == name).FirstOrDefault();

        }

    //----------> Update employee details

        public static void updateEmployee(CarEmployee te, CarPerson tp)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.Entry(te).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(tp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    //----------> Logged in employee can update some of his details

        public static void updatePersonalDetails(CarEmployee emp, CarPerson tp)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(tp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.SaveChanges();
            }
        }



    //----------> Rentals
    //----------> Rent car 

        public static void rentCar(CarRental tr, int id)
        {
            id = tr.CarId;
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                ctx.CarRentals.Add(tr);
                changeCarStatus(id);
                ctx.SaveChanges();
            }
        }
     //----------> Rental records by customer
        public static List<CarRentalsWithCustomerName> getRentalsByCustomer(int id)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())

            {
                return ctx.CarRentals.Where(i=>i.CustomerId ==id).Select(
                    record => new CarRentalsWithCustomerName()
                    {
                        RentalId = record.RentalId,
                        RegistrationNumber = record.Car.RegistrationNumber,
                        Name = record.Customer.Customer.Name,
                        RentDate = record.RentDate.ToString("dd/MM/yyyy"),                      
                        ReturnDueDate = record.ReturnDueDate.ToString("dd/MM/yyyy"),
                        ReturnDate = record.ReturnDate.ToString("dd/MM/yyyy"),
                        TotalPrice = string.Format("{0:F2}", record.TotalPrice),
                    }).ToList();
            }
        }

     //----------> All Rental records
        public static List<CarRentalsWithCustomerName> getRentals()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarRentals.Include(p => p.Customer).Select(
                    record => new CarRentalsWithCustomerName()
                    {
                        RentalId = record.RentalId,
                        RegistrationNumber = record.Car.RegistrationNumber,
                        Name = record.Customer.Customer.Name,
                        RentDate = record.RentDate.ToString("dd/MM/yyyy"),
                        ReturnDueDate = record.ReturnDueDate.ToString("dd/MM/yyyy"),
                        ReturnDate = record.ReturnDate.ToString("dd/MM/yyyy"),
                        TotalPrice = string.Format("{0:F2}", record.TotalPrice),
                    }).ToList();
            }
        }

    //----------> Rental records between 2 dates
        public static List<CarRentalsWithCustomerName> getRentalsBetweenDates(DateTime start, DateTime finish)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())

            {
                return ctx.CarRentals.Where(i =>i.RentDate>=start && i.RentDate<=finish).Select(
                    record => new CarRentalsWithCustomerName()
                    {
                        RentalId = record.RentalId,
                        RegistrationNumber = record.Car.RegistrationNumber,
                        Name = record.Customer.Customer.Name,
                        RentDate = record.RentDate.ToString("dd/MM/yyyy"),
                        ReturnDueDate = record.ReturnDueDate.ToString("dd/MM/yyyy"),
                        ReturnDate = record.ReturnDate.ToString("dd/MM/yyyy"),
                        TotalPrice = string.Format("{0:F2}", record.TotalPrice),
                    }).ToList();
            }
        }

    //----------> Find rental record by car
        public static CarRental findRentalRecordByCarRego(string rego)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
                return ctx.CarRentals.Include(p => p.Car).Where(car => car.Car.RegistrationNumber == rego).FirstOrDefault();

        }

    //----------> return car
        public static void returnCar (CarRental record)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {              
                ctx.Entry(record).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                int id = record.CarId;
                changeCarStatus(id);
                ctx.SaveChanges();
            }
                

        }

    //----------> Stored Procidures

    //----------> list of cars

        public static List<CarPerson> getPeople()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarPeople.FromSqlRaw("getAllPeople").ToList(); 
            }
        }

    //----------> top 5 rented cars
        public static List<TopFiveCars> getTopFiveCars()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.TopFiveCars.FromSqlRaw("topFiveCars").ToList();
            }
        }

    //----------> least 5 rented car models
        public static List<LeastFiveRentedModels> getLeastFiveRentedCarModels()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.LeastFiveRentedModels.FromSqlRaw("leastFiveRentedCarModels").ToList();
            }
        }

      //----------> get Car Models from PB database to display in the combobox
        public static List<CarModel> getCarModelsPB()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarModels.FromSqlRaw("getCarModels").ToList();
            }
        }

        //----------> get total price for the selected car model between 2 dates
        public static List<Total> getTotalPriceForSelectedCarModel( string model, DateTime start, DateTime end)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {               
                return ctx.Total.FromSqlRaw("EXECUTE dbo.getTotalRentForCarModel {0},{1},{2}", model, start, end).ToList();
            }
        }
        //----------> get Customers PB database to display in the combobox
        public static List<CarCustomer> getCarCustomersPB()
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.CarCustomers.FromSqlRaw("getCustomers").ToList();
            }
        }

        //----------> get total sale for customer by license numbr
        public static List<Total> getTotalCustomerRentByLicense(string license)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.Total.FromSqlRaw("getCustomerTotalRent {0}",license).ToList();
            }
        }

        //----------> get total sale by selected month and year
        public static List<Total> getTotalMonthlyRent(int month, int year)
        {
            using (DAD_CarSalesdbContext ctx = new DAD_CarSalesdbContext())
            {
                return ctx.Total.FromSqlRaw("getSalesByMonth {0}, {1}", month, year).ToList();
            }
        }

    }
}
