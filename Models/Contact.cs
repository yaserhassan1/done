using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Contact
    {
        public decimal Contactid { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
    }
}
