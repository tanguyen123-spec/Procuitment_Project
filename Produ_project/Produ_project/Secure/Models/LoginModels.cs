using System.ComponentModel.DataAnnotations;

namespace Produ_project.Secure.Models
{
    public class LoginModels
    {
        [Required]
        [MaxLength(255)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Passwords { get; set; } = string.Empty;
    }
}
