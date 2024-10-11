
using System.ComponentModel.DataAnnotations;

namespace TFL.Models
{
    public class User 
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
