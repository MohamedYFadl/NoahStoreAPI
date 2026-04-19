using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels
{
    public sealed record UserRoleViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Please enter at least 8 character, 1 uppercase, 1 lowercase, 1 number, 1 special characher")]

        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
