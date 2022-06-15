using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_Store.Models
{
    public partial class Productt
    {
        public Productt()
        {
            Cartts = new HashSet<Cartt>();
            Orderts = new HashSet<Ordert>();
        }

        public decimal Prodid { get; set; }
        public string Productname { get; set; }
        public string Description { get; set; }
        public DateTime? Datecreated { get; set; }
        public string Imagepath { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Price { get; set; }
        public decimal? Storeid { get; set; }
        public decimal? Quantity { get; set; }
        public char In_Stock { get; set; }
        public decimal? Rating { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Storet Store { get; set; }
        public virtual ICollection<Cartt> Cartts { get; set; }
        public virtual ICollection<Ordert> Orderts { get; set; }
    }
}
