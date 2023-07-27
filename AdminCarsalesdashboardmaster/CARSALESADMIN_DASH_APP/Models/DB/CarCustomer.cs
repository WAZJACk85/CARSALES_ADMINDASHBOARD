using System;
using System.Collections.Generic;

namespace NewDesignTrial.Models.DB
{
    public partial class CarCustomer
    {
        public CarCustomer()
        {
            CarRentals = new HashSet<CarRental>();
        }

        public int CustomerId { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public int Age { get; set; }

        private DateTime licenseExpiry;
        public DateTime LicenseExpiryDate
        {
            get {  return licenseExpiry; }
            set
            {
                if(value>DateTime.Today)
                {
                    licenseExpiry = value;
                }
                else
                {
                   // throw new Exception("License expired. Please provide new license.");
                }
            }
        }

        public virtual CarPerson Customer { get; set; } = null!;
        public virtual ICollection<CarRental> CarRentals { get; set; }
    }
}
