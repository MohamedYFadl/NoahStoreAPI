using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels
{
    public sealed record RoleViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(25,ErrorMessage = "Max. length is 25 charchaters")]
        public string Name { get; set; }
    }
}
