using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Core.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Please enter at least 8 character, 1 uppercase, 1 lowercase, 1 number, 1 special characher")]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        

    }
}
