using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Creditcard
    {
        public Creditcard()
        {
            Userts = new HashSet<Usert>();
        }

        public decimal Creditcardid { get; set; }
        public string Cardtype { get; set; }
        public string Cardnumber { get; set; }
        public DateTime? Expdata { get; set; }
        public byte? Cvv { get; set; }
        public DateTime? Modifieddate { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Usert> Userts { get; set; }
    }
}
