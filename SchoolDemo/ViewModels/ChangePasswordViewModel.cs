using System.ComponentModel.DataAnnotations;

namespace SchoolDemo.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="New password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm new password")]
        [Compare("NewPassword",ErrorMessage ="The new Password and confirmation password Do not match.")]
        public string ConfirmPassword { get; set;}
    }
}
