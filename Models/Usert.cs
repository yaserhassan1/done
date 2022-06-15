using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_Store.Models
{
    public partial class Usert
    {
        public Usert()
        {
            Cartts = new HashSet<Cartt>();
            Orderts = new HashSet<Ordert>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Userid { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = " Numbers or symbols are not allowed")]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Not a valid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; }
        public string Imagepath { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"[A-Za-z0-9]+",
         ErrorMessage = "Characters are not allowed.")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",
            ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter ,and at least 8 or more characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required ")]
        public DateTime? Barthday { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(14, ErrorMessage = "The phone number must be equal or less than 14 digit", MinimumLength = 10)]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,7}", ErrorMessage = "It must contain numbers only")]
        public string Phonenumber { get; set; }
      
        public string Gender { get; set; }
        public DateTime? Datecreated { get; set; }
        public string State { get; set; }
        public decimal? Roleid { get; set; }
        public decimal? AddresId { get; set; }
        public decimal? CarditcardId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Addresst Addres { get; set; }
        public virtual Creditcard Carditcard { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Cartt> Cartts { get; set; }
        public virtual ICollection<Ordert> Orderts { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
