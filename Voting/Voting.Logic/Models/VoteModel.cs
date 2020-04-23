using System;
using System.ComponentModel.DataAnnotations;

namespace Voting.Logic.Models
{
  public class VoteModel
  {
    public Guid? Id { get; set; }
    public UserModel User { get; set; }

    [Required]
    public Guid QuestionId { get; set; }
    public Guid? AnswerId { get; set; }
    public string AnswerText { get; set; }

    public VoteModel()
    {
      User = new UserModel();
    }
  }
}
