using System;

namespace Voiting.Repositories.Entities
{
  public class DbVote
  {
    public Guid Id { get; set; }
    public Guid AnswerId { get; set; }
    public Guid UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
