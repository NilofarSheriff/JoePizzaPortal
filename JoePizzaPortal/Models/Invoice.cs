using System;
using System.Collections.Generic;

namespace JoePizzaPortal.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Orders = new HashSet<Order>();
        }

        public int InvoiceId { get; set; }
        public int? InvUser { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double? TotalBill { get; set; }

        public virtual User? InvUserNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
