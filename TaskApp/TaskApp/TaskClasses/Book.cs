namespace TaskApp.TaskClasses
{
  public class Book
  {
    private readonly Author _author;
    private readonly Title _title;
    private readonly Content _content;

    public Book(Author author, Title title, Content content)
    {
      _author = author;
      _title = title;
      _content = content;
    }

    public void Show()
    {
      _author.Show();
      _title.Show();
      _content.Show();
    }
  }
}
