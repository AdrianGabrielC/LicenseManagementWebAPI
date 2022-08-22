using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly DatabaseContext dbContext;
        public CustomersController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLicenses()
        {
            var customers = await this.dbContext.CustomersTable.ToListAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicense(Customers customerRequest)
        {
            customerRequest.id = Guid.NewGuid();
            await this.dbContext.CustomersTable.AddAsync(customerRequest);
            await this.dbContext.SaveChangesAsync();
            return Ok(customerRequest);
        }
        

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await this.dbContext.CustomersTable.FindAsync(id);
            if (customer == null) return NotFound();
            this.dbContext.CustomersTable.Remove(customer);
            await this.dbContext.SaveChangesAsync();
            return Ok(customer);
        }
    }
}
