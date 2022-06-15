using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Addresst
    {
        public Addresst()
        {
            Userts = new HashSet<Usert>();
        }

        public decimal AddressId { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Usert> Userts { get; set; }
    }
}
