using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.Models
{
    public class Customer
    {        
        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }
    }
}