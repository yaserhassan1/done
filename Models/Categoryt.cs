using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_Store.Models
{
    public partial class Categoryt
    {
        public Categoryt()
        {
            Storets = new HashSet<Storet>();
        }

        public decimal Catid { get; set; }
        public string Categoryname { get; set; }
        public DateTime? Datecreated { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Storet> Storets { get; set; }
    }
}
