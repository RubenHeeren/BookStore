using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.DTOs
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]        
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Your password must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
