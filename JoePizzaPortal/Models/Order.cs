using System;
using System.Collections.Generic;

namespace JoePizzaPortal.Models
{
    public partial class Order
    {
        public int Orderid { get; set; }
        public int? ProductId { get; set; }
        public int? InvoiceNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderQty { get; set; }
        public double? OrderBill { get; set; }
        public double? OrderUnitPrice { get; set; }

        public virtual Invoice? InvoiceNoNavigation { get; set; }
        public virtual Pizza? Product { get; set; }
    }
}
