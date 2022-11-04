using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JoePizzaPortal.Models;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Web;


namespace JoePizzaPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly Joe_Pizza_PortalContext _context;

        public HomeController(Joe_Pizza_PortalContext context)
        {
            _context = context;
        }
        
        
        public ActionResult Index()
        {
            HttpContext.Session.SetInt32("UserId", 1);
           
            if (TempData["Cart"] != null)
            {
                float sum = 0;
                List<Cart> list2 = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["Cart"]);
                foreach (var item in list2)
                {

                    sum += item.bill;
                }
                TempData["total"] = sum;

            }
            TempData.Keep();
            return View(_context.Pizzas.OrderByDescending(x => x.ProductId).ToList());
        }

        public ActionResult Addtocart(int? Id)
        {

            Pizza prod = _context.Pizzas.Where(x => x.ProductId == Id).SingleOrDefault()!;
            return View(prod);
        }

        List<Cart> cartlist = new List<Cart>();
        [HttpPost]
        public ActionResult Addtocart(Pizza P, string qty, int Id)
        {

            Pizza prod = _context.Pizzas.Where(x => x.ProductId == Id).SingleOrDefault()!;
            Cart c = new Cart();
            c.ProductId = prod.ProductId;
            c.ProductName = prod.ProductName;
            c.Price = (float)prod.ProductPrice!;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.Price * c.qty;
            if (TempData["Cart"] == null)
            {
                cartlist.Add(c);

                TempData["Cart"] = JsonConvert.SerializeObject(cartlist);

            }
            else
            {
                List<Cart> list2 = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["Cart"]) as List<Cart>;
                int flag = 0;
                foreach (var item in list2)
                {
                    if (item.ProductId == c.ProductId)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    list2.Add(c);

                }

                TempData["Cart"] = JsonConvert.SerializeObject(list2);

            }

            TempData.Keep();

            return RedirectToAction("Index");

        }

        public ActionResult CheckOut()
        {

            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(Order odr)
        {

            //List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["Cart"])as List<Cart>;
            //Invoice Fi = new Invoice();
            //Fi.InvUser = HttpContext.Session.GetInt32("UserId");
            //Fi.InvoiceDate = System.DateTime.Now;
            //Fi.TotalBill = (double?)TempData["total"];
            //_context.Invoices.Add(Fi);
            //_context.SaveChanges();
            //foreach (var item in cart)
            //{
            //    Order o = new Order();
            //    o.ProductId = item.ProductId;
            //    o.InvoiceNo = Fi.InvoiceId;
            //    o.OrderDate = System.DateTime.Now;
            //    o.OrderQty = item.qty;
            //    o.OrderUnitPrice = (int)item.Price;
            //    o.OrderBill = item.bill;
            //    _context.Orders.Add(o);
            //    _context.SaveChanges();
            //}
            //TempData.Remove("total");
            //TempData.Remove("Cart");

            //TempData["msg"] = "Laptop Ordered Successfully";
            //TempData.Keep();
            TempData["msg"] = "Pizza Ordered Successfully";


            return RedirectToAction("Index");
        }

        public ActionResult remove(int id)
        {
            List<Cart> cart = TempData["Cart"] as List<Cart>;
            Cart c = cart.Where(X => X.ProductId == id).SingleOrDefault();
            cart.Remove(c);
            float a = 0;
            foreach (var item in cart)
            {
                a += item.bill;
            }
            TempData["total"] = JsonConvert.SerializeObject(a);
            return RedirectToAction("CheckOut");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //GET: Home
        

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductImage")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductImage")] Pizza pizza)
        {
            if (id != pizza.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pizzas == null)
            {
                return Problem("Entity set 'Joe_Pizza_PortalContext.Pizzas'  is null.");
            }
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.ProductId == id);
        }
    }
}
