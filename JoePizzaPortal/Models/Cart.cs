namespace JoePizzaPortal.Models
{
    public class Cart
    {
        
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public float Price { get; set; }

            public int qty { get; set; }

            public float bill { get; set; }
        
    }
}
