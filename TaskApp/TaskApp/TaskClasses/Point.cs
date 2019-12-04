namespace TaskApp.TaskClasses
{
  public class Point
  {
    private int _x;
    private int _y;
    private string _title;

    public int X { get => _x; }
    public int Y { get => _y; }
    public string Title { get => _title; }

    public Point(int x, int y, string title)
    {
      this._x = x;
      this._y = y;
      this._title = title;
    }
  }
}
