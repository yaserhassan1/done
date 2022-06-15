using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_Store.Models
{
    public partial class Storet
    {
        public Storet()
        {
            Productts = new HashSet<Productt>();
        }

        public decimal Storeid { get; set; }
        public string Storename { get; set; }
        public string Description { get; set; }
        public DateTime? Datecreated { get; set; }
        public string Imagepath { get; set; }
        public string State { get; set; }
        public decimal? Rating { get; set; }
        public decimal? Catid { get; set; }
        public DateTime? Dateopen { get; set; }
        public DateTime? Dateclose { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Categoryt Cat { get; set; }
        public virtual ICollection<Productt> Productts { get; set; }
    }
}
