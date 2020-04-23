using System.ComponentModel.DataAnnotations;

namespace Voting.Logic.Models
{
  public class RegisterUserModel : LoginUserModel
  {
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
  }
}
