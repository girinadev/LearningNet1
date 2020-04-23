using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Logic.Models
{
  public class SearchFilter
  {
    public QuestionStatuses? Status { get; set; }

    public DateTime? VotingEndDate { get; set; }

    public Guid? UserId { get; set; }
    public bool? IsVoted { get; set; }
  }
}
