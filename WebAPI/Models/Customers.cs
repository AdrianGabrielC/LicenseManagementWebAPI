using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Customers
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
