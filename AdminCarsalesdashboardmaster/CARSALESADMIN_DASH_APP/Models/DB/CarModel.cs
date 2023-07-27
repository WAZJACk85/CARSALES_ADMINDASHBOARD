using System;
using System.Collections.Generic;

namespace NewDesignTrial.Models.DB
{
    public partial class CarModel
    {
        public CarModel()
        {
            IndividualCars = new HashSet<IndividualCar>();
        }

        public int ModelId { get; set; }
        public string Model { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string Size { get; set; } = null!;
        private int seat;
        public int Seats
        {
            get { return seat; }
            set
            {
                if(value>1)
                {
                    seat = value;
                }
                else
                {
                    throw new Exception("Please check the seat count");
                }
            }
        }
        private int door;
        public int Doors
        {
            get { return door; }
            set
            {
                if(value >= 1)
                {
                    door = value;
                }
                else
                {
                    throw new Exception("Please check the doors count");
                }
            }
        }

        public virtual ICollection<IndividualCar> IndividualCars { get; set; }
    }
}
