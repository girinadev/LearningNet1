namespace TaskApp.TaskClasses
{
  public class Address
  {
    private int index;
    private string country;
    private string city;
    private string street;
    private string house;
    private string appartment;

    public int Index { get => index; set => index = value; }

    public string Country { get => country; set  => country = value; }

    public string City { get => city; set => city = value; }

    public string Street { get => street; set => street = value; }

    public string House { get => house; set => house = value; }

    public string Appartment { get => appartment; set => appartment = value; }
  }
}
