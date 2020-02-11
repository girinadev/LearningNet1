namespace TaskApp.Generics
{
  public class Car
  {
    public int MadeYear { get; set; }
    public string Title { get; set; }

    public override string ToString()
    {
      return $"{Title} {MadeYear}";
    }
  }
}
