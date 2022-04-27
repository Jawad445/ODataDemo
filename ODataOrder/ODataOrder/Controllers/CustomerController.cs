using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataOrder.Data;

//URL FOR TESTING
//https://localhost:7183/api/Customer?$filter=countryeq'CH'&$orderby=CustomerName&$expand=Orders

namespace ODataOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ODataController
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get() {
            var xyz = HttpContext.Request.QueryString;
            return Ok( _context.Customers);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer item)
        {
            _context.Customers.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }


        [HttpPost]
        [Route("fill")]
        public async Task<IActionResult> Fill()
        {
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                var c = new Customer
                {
                    CustomerName = demoCustomers[rand.Next(demoCustomers.Count)],
                    Country = demoCountries[rand.Next(demoCountries.Count)]
                };
                _context.Customers.Add(c);

                for (var j = 0; j < 10; j++)
                {
                    var o = new Order
                    {
                        OrderDate = DateTime.Today,
                        ProductName = demoProducts[rand.Next(demoProducts.Count)],
                        Quantity = rand.Next(1, 5),
                        Revenue = rand.Next(100, 5000),
                        customer = c
                    };
                    _context.Orders.Add(o);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private readonly List<string> demoCustomers = new List<string>
        {
            "Foo",
            "Bar",
            "Acme",
            "King of Tech",
            "Awesomeness"
        };

        private readonly List<string> demoProducts = new List<string>
        {
            "Bike",
            "Car",
            "Apple",
            "Spaceship"
        };

        private readonly List<string> demoCountries = new List<string>
        {
            "AT",
            "DE",
            "CH"
        };
    }
}
