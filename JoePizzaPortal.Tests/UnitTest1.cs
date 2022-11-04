using JoePizzaPortal.Models;
using JoePizzaPortal;

namespace JoePizzaPortal.Tests
{
    [TestFixture]
    public class Test
    {

        [Test]
        public void PizzaAdd()
        {
            Pizza pizza1 = new Pizza();
            pizza1.ProductName = "Veggie Pizza";
            pizza1.ProductPrice = 250;
            pizza1.ProductImage = "~/Images/9.jpg";
            pizza1.ProductDescription = "When you want to jazz up your cheese pizza with color and texture, veggies are the perfect topping. And you’re only limited by your imagination. Everything from peppers and mushrooms, to eggplant and onions make for an exciting and tasty veggie pizza.";

            bool ans = pizza1.AddPizza(pizza1);
            Assert.IsTrue(ans);

        }
        [Test]
        public void PizzaAvailabiltiyCheck()
        {
            Pizza one = new Pizza();
            bool ans = one.AvailabilityCheck();
            Assert.AreEqual(ans,true);
        }
       
    }
}