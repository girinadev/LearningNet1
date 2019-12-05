using System.Linq;

namespace TaskApp.TaskInheritance
{
  public class ClassRoom
  {
    private const int _maxCount = 4;
    private Pupil[] _pupils = new Pupil[_maxCount];

    public ClassRoom(params Pupil[] pupils)
    {
      (pupils.Length > _maxCount ? pupils.Take(_maxCount) : pupils).ToArray().CopyTo(_pupils, 0);
    }

    public void PrintInfoAboutPupils()
    {
      foreach (var p in _pupils.Where(p => p != null))
      {
        p.Study();
        p.Read();
        p.Write();
        p.Relax();
      }
    }
  }
}
