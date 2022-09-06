using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Owner:EntityBase
    { 
        public string FullName { get; set; }
        public string Profilo { get; set; }
        public string Avatar { get; set; }
        public Address Address { get; set; }
    }
}
