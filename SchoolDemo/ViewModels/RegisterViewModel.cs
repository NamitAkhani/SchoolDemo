using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolDemo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]  
        [Remote(action:"IsEmailInUse",controller:"Account")]
        public string Email { get; set; }  
        [Required]  
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm Password Do not Match")]
        public string ConfirmPassword { get; set; } 
        public string Role { get; set; }    
    }
}
