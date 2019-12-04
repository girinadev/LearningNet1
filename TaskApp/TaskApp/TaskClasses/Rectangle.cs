namespace TaskApp.TaskClasses
{
  public class Rectangle
  {
    private readonly double _side1;
    private readonly double _side2;
    private double _area;
    private double _perimeter;

    public double Area => _area;
    public double Perimeter => _perimeter;

    public Rectangle(double side1, double side2)
    {
      this._side1 = side1;
      this._side2 = side2;
    }

    public double AreaCalculator()
    {
      this._area = this._side1 * this._side2;
      return this._area;
    }

    public double PerimeterCalculator()
    {
      this._perimeter = this._side1 * 2 + this._side2 * 2;
      return this._perimeter;
    }
  }
}
