using System.ComponentModel.DataAnnotations;

namespace GistPage.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]  
        public string? UserName { get; set; }

        [Required]  
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
