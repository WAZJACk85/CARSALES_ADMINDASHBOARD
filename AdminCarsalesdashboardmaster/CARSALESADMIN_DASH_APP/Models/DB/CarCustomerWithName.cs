﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NewDesignTrial.Models.DB
{
    public class CarCustomerWithName
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public int Age { get; set; }
        public string LicenseExpiryDate { get; set; }
    }
}
