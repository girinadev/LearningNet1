using System;

namespace Voting.Logic.Models
{
  public class UserModel
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }   
    public string LastName { get; set; }
    public bool CanVote { get; set; }
  }
}
