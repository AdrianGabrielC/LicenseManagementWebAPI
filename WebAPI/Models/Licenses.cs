using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Licenses
    {
        [Key]
        public Guid id { get; set; }
        public string key { get; set; }
        public string type { get; set; }
        public string duration { get; set; }
        public string product { get; set; }
        public string owner { get; set; }
        public string status { get; set; }

    }
}
