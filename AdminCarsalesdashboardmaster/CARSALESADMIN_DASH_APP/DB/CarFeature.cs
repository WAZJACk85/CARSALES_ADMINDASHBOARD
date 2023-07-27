using System;
using System.Collections.Generic;

namespace NewDesignTrial.Models.DB
{
    public partial class CarFeature
    {
        public CarFeature()
        {
            Cars = new HashSet<IndividualCar>();
        }

        public int FeatureId { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<IndividualCar> Cars { get; set; }
    }
}
