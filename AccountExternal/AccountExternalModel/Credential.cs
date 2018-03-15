using BaseModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountExternalModel
{
    public class Credential : Base
    {
        public bool IsActive { get; set; }
        public int CredentialId { get; set; }
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Incorrect Password.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Salt { get; set; }
        
        public string Username { get; set; }


        public List<CredentialRole> CredentialRoles { get; set; }
    }
}
