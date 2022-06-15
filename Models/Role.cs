using System;
using System.Collections.Generic;

#nullable disable

namespace Market_Store.Models
{
    public partial class Role
    {
        public Role()
        {
            Userts = new HashSet<Usert>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Usert> Userts { get; set; }
    }
}
