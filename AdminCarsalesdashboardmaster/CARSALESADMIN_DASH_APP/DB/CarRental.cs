using System;
using System.Collections.Generic;

namespace NewDesignTrial.Models.DB
{
    public partial class CarRental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        
        decimal total;
        public decimal TotalPrice
        {
            get { return total; }
            set
            {
                if(value>=50 && value<=60000)  //----> changed from 50 and from 10000
                {
                    total = value;
                }
                else
                {
                    throw new Exception("Please check the total price");
                }
            }
        }

        public virtual CarCustomer Customer { get; set; } = null!;
        public virtual IndividualCar Car { get; set; } = null!;
    }
}
