using System;
using System.Collections.Generic;

namespace JoePizzaPortal.Models
{
    public partial class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int UId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Contact { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
