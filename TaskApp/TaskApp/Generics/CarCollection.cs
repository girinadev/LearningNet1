namespace TaskApp.Generics
{
  public class CarCollection<T> : MyList<T> where T : Car, new()
  {
    public void AddCar(int madeYear, string title)
    {
      var car = new T
      {
        MadeYear = madeYear,
        Title = title
      };

      Add(car);
    }

    public void ClearAllCars()
    {
      Clear();
    }
  }
}
