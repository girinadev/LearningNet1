using System;
using System.Collections.Generic;

namespace TaskApp.Generics
{
  public class SomeClass : IEqualityComparer<SomeClass>, ICloneable
  {
    public string F1 { get; set; }
    public string F2 { get; set; }

    public SomeClass()
    {
      F1 = "F1";
      F2 = "F2";
    }

    public SomeClass(string f1, string f2)
    {
      F1 = f1;
      F2 = f2;
    }

    public override string ToString()
    {
      return $"{F1} {F2}";
    }

    public override bool Equals(object obj)
    {
      return obj is SomeClass c && (c.F1.Equals(F1) && c.F2.Equals(F2));
    }

    public override int GetHashCode()
    {
      return F1.GetHashCode() & F2.GetHashCode();
    }

    public bool Equals(SomeClass x, SomeClass y)
    {
      return x.F1.Equals(F1) && y.F2.Equals(F2);
    }

    public int GetHashCode(SomeClass obj)
    {
      return obj.GetHashCode();
    }

    public object Clone()
    {
      return new SomeClass(F1, F2);
    }
  }
}
