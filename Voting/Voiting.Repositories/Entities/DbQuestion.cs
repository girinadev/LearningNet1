using System;

namespace Voiting.Repositories.Entities
{
  public class DbQuestion
  {
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }    
    public int MaxVoteCount { get; set; }
    public int MaxAnswersCount { get; set; }   
    public DateTime? VotingEndDate { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
