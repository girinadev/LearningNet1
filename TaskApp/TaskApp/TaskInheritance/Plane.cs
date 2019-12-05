namespace TaskApp.TaskInheritance
{
  public class Plane : Vehicle
  {
    public decimal Height { get; set; }
    public int PassengersCount { get; set; }

    public override string ToString()
    {
      return $"{base.ToString()} Height={Height}, PassengersCount={PassengersCount}";
    }
  }
}
