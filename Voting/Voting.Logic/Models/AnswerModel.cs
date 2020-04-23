using System;
using System.Collections.Generic;

namespace Voting.Logic.Models
{
  public class AnswerModel
  {
    public Guid? Id { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }

    public IEnumerable<VoteModel> Votes { get; set; }

    public int Total { get; set; }
  }
}
