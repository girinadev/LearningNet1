using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Voting.Logic.Models
{
  public class QuestionModel
  {
    public Guid? Id { get; set; }

    [Required]
    public string Text { get; set; }

    public UserModel User { get; set; }

    [Required]
    public QuestionTypes Type { get; set; }

    [Required]
    public QuestionStatuses Status { get; set; }

    [Required]
    public int MaxVoteCount { get; set; }

    [Required]
    public int MaxAnswersCount { get; set; }
    
    public DateTime? VotingEndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public AnswerModel[] Answers { get; set; }

    public int Total => Answers?.Sum(a => a.Total) ?? 0;

    public QuestionModel()
    {
      User = new UserModel();
    }
  }
}
