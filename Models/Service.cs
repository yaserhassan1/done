using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_Store.Models
{
    public partial class Service
    {
        public decimal ServiceId { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
