using System;

namespace TaskApp.TaskClasses
{
  public class Figure
  {
    private readonly Point _p5;
    private readonly Point _p4;
    private readonly Point _p1;
    private readonly Point _p2;
    private readonly Point _p3;

    public Figure(Point p1, Point p2, Point p3)
    {
      _p1 = p1;
      _p2 = p2;
      _p3 = p3;
    }

    public Figure(Point p1, Point p2, Point p3, Point p4) : this(p1, p2, p3)
    {
      _p4 = p4;
    }

    public Figure(Point p1, Point p2, Point p3, Point p4, Point p5) : this(p1, p2, p3, p4)
    {
      _p5 = p5;
    }

    public double PerimeterCalculator()
    {
      if (_p5 != null)
        return LengthSide(_p1, _p2) + LengthSide(_p2, _p3) + LengthSide(_p3, _p4) + LengthSide(_p4, _p5) + LengthSide(_p5, _p1);

      if (_p4 != null)
        return LengthSide(_p1, _p2) + LengthSide(_p2, _p3) + LengthSide(_p3, _p4) + LengthSide(_p4, _p1);

      return LengthSide(_p1, _p2) + LengthSide(_p2, _p3) + LengthSide(_p3, _p1);
    }

    public double LengthSide(Point pFrom, Point PTo) => Math.Sqrt(Math.Pow(PTo.X - pFrom.X, 2) + Math.Pow(PTo.Y - pFrom.Y, 2));

    public override string ToString()
    {
      return string.Join(" ", new[] { _p1.Title, _p2.Title, _p3.Title, _p4?.Title, _p5?.Title }).Trim();
    }
  }
}
