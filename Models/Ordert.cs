using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Ordert
    {
        public decimal Orderid { get; set; }
        public string Codename { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Prodid { get; set; }
        public DateTime? Datecreated { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual Productt Prod { get; set; }
        public virtual Usert User { get; set; }
    }
}
