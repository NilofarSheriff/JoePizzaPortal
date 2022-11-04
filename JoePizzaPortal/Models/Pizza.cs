using System;
using System.Collections.Generic;

namespace JoePizzaPortal.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double? ProductPrice { get; set; }
        public string ProductDescription { get; set; } = null!;
        public string? ProductImage { get; set; }

        public bool AddPizza(Pizza pizza1)
        {
            try
            {
                Joe_Pizza_PortalContext context = new Joe_Pizza_PortalContext();
                context.Pizzas.Add(pizza1);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;

            }
            


        }

        public bool AvailabilityCheck()
        {
            
                Joe_Pizza_PortalContext context = new Joe_Pizza_PortalContext();
                List<Pizza> Pizzas = context.Pizzas.ToList();
                if(Pizzas.Count > 0)
                {
                    return true;

                }
                else
                {
                return false;
                 }
            

         }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
