using System;
using System.Collections.Generic;

namespace NewDesignTrial.Models.DB
{
    public partial class CarPerson
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Telephone { get; set; } = null!;

        public virtual CarCustomer CarCustomer { get; set; } = null!;
        public virtual CarEmployee CarEmployee { get; set; } = null!;
    }
}
