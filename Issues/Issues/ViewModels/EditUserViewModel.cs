using System.ComponentModel.DataAnnotations;

namespace Issues.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(150)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [MaxLength(150)]
        public string Job { get; set; }
    }
}