using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portable.Licensing;
using System.Text;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LicensesController : Controller
    {
        private readonly DatabaseContext dbContext;
        private readonly string privateKey = "MIIBIDAjBgoqhkiG9w0BDAEDMBUEEEGL0WkjWKX20vPotLOaG5ICAQoEgfh91sQGC+VLezY6Uygo1lyzDqeuVPhpDUXYTpjKBfy++Ebu9lK7vegq1648nDFN6PFLH7caQ2GWSg8/zie9p81AXkdMo2b1Rqkf0MOCY6N+wY8/4VQQvzyr8LEujZbDofzKTcQCc7dn/982PyuNIZjyAVRkb/7VfPsOvHKjsxYVLAjoxBoRN9HTgSdekVTBcefBU4rI+X5+otPks0a9tOSGNkUADPVldBs7WTZjLzWg377LNCt/1FJtj05qggvrldLyN8/G4eFFAWulAFS+ybFNrFVaaH64IWCRgFblGzgWE4LgJe9d2mqrsT+NlcepPndSGjxD91+lvA==";
        public LicensesController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLicenses()
        {
            var licenses = await this.dbContext.LicensesTable.ToListAsync();
            return Ok(licenses);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicense(Licenses licenseRequest)
        {
            var license = Portable.Licensing.License.New()
                .WithUniqueIdentifier(Guid.NewGuid())
                .As(LicenseType.Trial)
                .ExpiresAt(DateTime.Now.AddMilliseconds(1))
                .WithMaximumUtilization(1)
                .WithProductFeatures(new Dictionary<string, string>
                                  {
                                      {"Product name:", licenseRequest.product}
                                  })
                .LicensedTo(licenseRequest.owner, licenseRequest.owner)
                .CreateAndSignWithPrivateKey(this.privateKey, "passphrase");
            licenseRequest.id = Guid.NewGuid();
            licenseRequest.key = license.GetHashCode().ToString();
            System.IO.File.WriteAllText("License.lic", license.ToString(), Encoding.UTF8);
            await this.dbContext.LicensesTable.AddAsync(licenseRequest);
            await this.dbContext.SaveChangesAsync();
            return Ok(licenseRequest);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteLicense(Guid id)
        {
            var license = await this.dbContext.LicensesTable.FindAsync(id);
            if (license == null) return NotFound();
            this.dbContext.LicensesTable.Remove(license);
            await this.dbContext.SaveChangesAsync();
            return Ok(license);
        }
    }
}
