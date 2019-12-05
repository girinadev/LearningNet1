namespace TaskApp.TaskInheritance
{
  public abstract class Vehicle
  {
    public double X { get; set; }
    public double Y { get; set; }
    public double Price { get; set; }
    public double Speed { get; set; }
    public double YearOfIssue { get; set; }

    public override string ToString()
    {
      return $"X={X}, Y={Y}, Price={Price}, Speed={Speed}, YearOfIssue={YearOfIssue}";
    }
  }
}
