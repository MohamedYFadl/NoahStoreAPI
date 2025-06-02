using System.ComponentModel.DataAnnotations;

namespace NoahStore.Core.Dto
{
    public sealed record LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Please enter at least 8 character, 1 uppercase, 1 lowercase, 1 number, 1 special characher")]
        public string Password { get; set; }
    }
}