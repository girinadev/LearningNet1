using System;

namespace Voiting.Repositories.Entities
{
  public class DbUser
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
