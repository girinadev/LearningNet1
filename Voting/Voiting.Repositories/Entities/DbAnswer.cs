using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voiting.Repositories.Entities
{
  [Table("dbo.Answer")]
  public class DbAnswer
  {    
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}
