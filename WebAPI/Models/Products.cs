using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Products
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string shortCode { get; set; }
        public string licenseType { get; set; }
    }
}
