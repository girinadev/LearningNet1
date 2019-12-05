namespace TaskApp.TaskInheritance
{
  public class Ship : Vehicle
  {
    public string Port { get; set; }
    public int PassengersCount { get; set; }

    public override string ToString()
    {
      return $"{base.ToString()} Port={Port}, PassengersCount={PassengersCount}";
    }
  }
}
