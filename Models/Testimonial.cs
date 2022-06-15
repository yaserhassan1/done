using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Testimonial
    {
        public decimal TestimonialId { get; set; }
        public string TestimonialComment { get; set; }
        public decimal? Userid { get; set; }
        public DateTime? Modifieddate { get; set; }

        public virtual Usert User { get; set; }
    }
}
