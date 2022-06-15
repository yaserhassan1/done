using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Cartt
    {
        public decimal CartId { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? UserId { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? Datecreated { get; set; }

        public virtual Productt Product { get; set; }
        public virtual Usert User { get; set; }
    }
}
