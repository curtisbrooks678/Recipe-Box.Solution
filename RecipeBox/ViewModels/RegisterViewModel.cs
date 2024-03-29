using System.ComponentModel.DataAnnotations;

namespace RecipeBox.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
  
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}