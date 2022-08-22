using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext dbContext;
        public ProductsController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLicenses()
        {
            var products = await this.dbContext.ProductsTable.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicense(Products productRequest)
        {
            productRequest.id = Guid.NewGuid();
            await this.dbContext.ProductsTable.AddAsync(productRequest);
            await this.dbContext.SaveChangesAsync();
            return Ok(productRequest);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await this.dbContext.ProductsTable.FindAsync(id);
            if (product == null) return NotFound();
            this.dbContext.ProductsTable.Remove(product);
            await this.dbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
