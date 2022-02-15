using System.ComponentModel.DataAnnotations;

namespace SchoolDemo.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
         
    }
}
